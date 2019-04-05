using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = .1f;
    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;

    // Start() is called before the first frame update
    // It's "protected virtual" here so that it can be overwritten by inheriting classes
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;

    }

    // Let's get things moving!
    // "out" keyword means we're returning this value in addition to the boolean value
    protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
    {

        // Get the start position, and then calculate end position
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        // Disable the collider, so we won't hit our own collider
        boxCollider.enabled = false;
        // Check if anything would be hit while moving
        hit = Physics2D.Linecast(start, end, blockingLayer);
        // Re-enable the collider
        boxCollider.enabled = true;

        // If hit is null, we can move somewhere!
        if (hit.transform == null)
        {
            // Make the movement smoooooth with a coroutine
            StartCoroutine(SmoothMovement(end));
            // Movement was successful, so return true
            return true;
        }

        // Movement was unsuccessful, so return false
        return false;
    }

    // Coroutine for element movement
    // "end" = where to move to
    protected IEnumerator SmoothMovement (Vector3 end)
    {
        // Calculate the distance to move
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon) {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            // Move the thing
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

    protected virtual void AttemptMove <T> (int xDir, yDir)
        where T: Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
            return;

        // Hit component is generic, cause anything can hit anything
        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
            OnCantMove(hitComponent);
    }

    // Hey, this thing can't move!
    protected abstract void OnCantMove <T> (T component)
        where T : Component;

}
