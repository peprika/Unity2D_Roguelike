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
    // It's "protected virtual" so that it can be overwritten by inheriting classes
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;

    }

    // Coroutine for element movement
    // "end" = where to move to
    protected IEnumerator SmoothMovement (Vector3 end)
    {
        // Calculate the distance to move
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while sqrRemaingDistance > float.Epsilon {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
