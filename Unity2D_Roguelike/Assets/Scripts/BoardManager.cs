using UnityEngine;
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
    public GameObject[] enemyTiles;
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
        
        // For each grid position...
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                // Select a floor tile
                GameObject toInstantiate = floorTiles[Random.Range (0, floorTiles.Length)];
                // If we're at the edge of the gameboard, choose outerwall tile instead
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                // Now that the floor tile is selected, it's time to instantiate it
                GameObject instace = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                // Set the instantiated floor tile's parent to boardHolder
                instace.transform.SetParent(boardHolder);
            }
        }
    }

    // This function returns a random position on the gameboard
    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridpositions.Count);
        Vector3 randomPosition = gridpositions[randomIndex];
        // Remove the grid position (so that two objects won't spawn at the same location)
        gridpositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    // Spawn tiles at random positions
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        // How many objects will be spawned
        int objectCount = Random.Range(minimum, maximum + 1);

        // Spawn them at random locations
        for (int i = 0; i < objectCount; i ++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    // The only public function of this class
    // This will be called to actually set up the scene for each level
    public void SetupScene (int level)
    {
        BoardSetup();
        InitializeList();

        // Spawn random number of walls and food
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);

        // Number of enemies depends on the level
        int enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

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
