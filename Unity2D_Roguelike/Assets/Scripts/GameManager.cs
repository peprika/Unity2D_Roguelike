﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;
    public int plyaerFoodPoints = 100;

    private int level = 3;

    // Use this for initialization
    void Awake()
    {
        // Make sure there aren't two GameManager instances, and retain it between scenes
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
 
        // Get game board and initialize game
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        boardScript.SetupScene(level);
    }

}
