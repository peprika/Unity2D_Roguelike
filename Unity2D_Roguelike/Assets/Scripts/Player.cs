﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    // How much damage chop inflicts to walls
    public int wallDamage = 1;

    // Food items' point values
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;
    
    // Stores player's score during a level
    public int food;

    public float restartLevelDelay = 1f;

    private Animator animator;


    // Start is called before the first frame update.
    // "protected override" here, because we'll have a different implementation than in MovingObject class
    protected override void Start()
    {
        animator = GetComponent<Animator>();

        // Get the player's current food points
        food = GameManager.instance.playerFoodPoints;

        // Call Start() of MovingObject
        base.Start();
    }

    // OnDisable() is part of Unity API: it's called when Player is disabled
    // Saves the value of "food" in the GameManager when the level changes
    private void OnDisable()
    {
            GameManager.instance.playerFoodPoints = food;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        // Every move takes up 1 food
        food--;

        // Try to move, as per MovingObject
        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit;

        // Since food was lost -- is it game over already?
        CheckIfGameOver();

        // End of player's turn
        GameManager.instance.playersTurn = false;
    }

    private void CheckIfGameOver()
    {
        if (food <= 0)
            GameManager.instance.GameOver();
    }
}
