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


    // Start is called before the first frame update
    // "protected override", because we're overriding MovingObject's Start()
    protected override void Start()
    {
        // Get animator
        animator = GetComponent<Animator>();

        // Get player's position to target
        target = GameObject.FindGameObjectWithTag("Player").transform;

        // Call Start() of MovingObject
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
