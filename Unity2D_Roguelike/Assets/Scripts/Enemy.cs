using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{

    // Declarations
    public int playerDamage;    // Damage done to the player (food points)

    private Animator animator;  // Animator
    private Transform target;   // Player's position, aka enemy's target
    private bool skipMove;      // skipMove is used so that enemy moves every other turn


    // Start() is called before the first frame update
    // "protected override" here, because we're overriding MovingObject's Start()
    protected override void Start()
    {
        // Add the enemy to the list of enemies in GameManager
        GameManager.instance.AddEnemyToList(this);

        // Get animator
        animator = GetComponent<Animator>();

        // Get player's position to target
        target = GameObject.FindGameObjectWithTag("Player").transform;

        // Call Start() of MovingObject
        base.Start();
    }

    // The enemy tries to move
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        // Enemy skips every other turn
        if (skipMove)
        {
            skipMove = false;
            return;
        }

        // Now the enemy is ready to move
        // T = player (aka move towards player)
        base.AttemptMove<T>(xDir, yDir);

        // Enemy has moved
        skipMove = true;
    }

    // Called by GameManager when enemies move
    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;

        // Check if player's X-position is roughly the same as enemy's X-position
        // aka: is the enemy in the same column as the player?
        if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
        {
            // If so, move vertically towards player (1 = up, -1 = down)
            yDir = target.position.y > transform.position.y ? 1 : -1;
        }
        else
        {
            // If not, move horizontally towards player (1 = right, -1 = left)
            xDir = target.position.x > transform.position.x ? 1 : -1;
        }

        // Now the enemy tries to move
        AttemptMove<Player>(xDir, yDir);
    }

    // The enemy can't move!
    // Override MovingObject's abtract OnCantMove()
    protected override void OnCantMove<T>(T component)
    {
        // Get the player object to hit
        Player hitPlayer = component as Player;

        // Play the enemy's attack animation
        animator.SetTrigger("enemyAttack");

        // The player loses food!
        hitPlayer.LoseFood(playerDamage);
    }

}
