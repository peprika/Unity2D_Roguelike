﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class BoardManager : MonoBehaviour
{
    // Used to generate min/max amount of elements
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    // Some basic declarations
    public int columns = 8;
    public int rows = 8;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] emptyTiles;
    public GameObject[] outerWallTiles;

    // Boardholder is used to maintain game board's hierarchy clean
    private Transform boardHolder;
    
    // Used to track game board positions
    private List<Vector3> gridpositions = new List<Vector3>();

    void InitializeList()
    {
        gridpositions.Clear();

        // Map out grid positions on game board
        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < -rows - 1; y++)
            {
                gridpositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    // BoardSetup() sets the outer walls and gameboard floor
    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows; y++)
            {

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
