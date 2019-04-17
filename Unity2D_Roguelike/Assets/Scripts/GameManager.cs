using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;

    public float levelDelay = .2f;                      // How many seconds to wait between each level
    public float turnDelay = .1f;                       // How many seconds to wait between each turn
    public int playerFoodPoints = 100;                  // Player's food points
    [HideInInspector] public bool playersTurn = true;   // true if player's turn

    private int level = 1;                              // Current level
    private List<Enemy> enemies;                        // List of enemies
    private bool enemiesMoving;                         // true if enemies are moving
    private GameObject levelImage;                      // Title screen between levels
    private Text levelText;                             // Text in the title card between levels ("Day X")
    private bool doingSetup;                            // true if user is doing setup

    // Use this for initialization
    void Awake()
    {
        // Make sure there aren't two GameManager instances, and retain it between scenes
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        // Initialize enemy list
        enemies = new List<Enemy>();

        // Get game board and initialize game
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    // OnLevelWasLoaded is called every time a scene is loaded
    private void OnLevelWasLoaded(int index)
    {
        // Level number up
        level++;

        // Initialize a new level
        InitGame();
    }

    void InitGame()
    {
        // Player can't move while the title card is showing
        doingSetup = true;

        // Get references to level image+text
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();

        // Set level text and show the title screen
        levelText.text = "Day " + level;
        levelImage.SetActive(true);

        // Clear enemies from the last level
        enemies.Clear();
        boardScript.SetupScene(level);
    }

    public void GameOver()
    {
        // Disables the GameManager
        enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // If it's the player's turn or enemies are already moving, don't do anything for now
        if (playersTurn || enemiesMoving)
            return;
        // The enemies are ready to move
        StartCoroutine(MoveEnemies());
    }

    // Add enemies to list, so they can be managed from there
    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    // Coroutine for moving enemies
    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;

        // Wait turn delay
        yield return new WaitForSeconds(turnDelay);

        // If there are no enemies, wait still
        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        // Move each enemy, and wait their move time
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        // The enemies have moved, it's the player's turn now
        playersTurn = true;
        enemiesMoving = false;
    }
}
