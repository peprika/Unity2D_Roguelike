﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    // How much damage chop inflicts to walls
    public int wallDamage = 1;

    // Food items' point values
    public int pointsPerFood = 10;
    public int pointsperSoda = 20;
    
    // Stores player's score during a level
    public int food;

    public float restartLevelDelay = 1f;

    private Animator animator;


    // Start is called before the first frame update.
    // "protected override" here, because we'll have a different implementation than in MovingObject class
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}