using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;
    public float turnDelay = .1f;
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;

    private int level = 3;
    private List<Enemy> enemies;
    private bool enemiesMoving;

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
        // Clear enemies from the last level
        enemies.Clear();
        boardScript.SetupScene(level);
    }

    public void GameOver()
    {
        // Disables the GameManager
        enabled = false;
    }

}
