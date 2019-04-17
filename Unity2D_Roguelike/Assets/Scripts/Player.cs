using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovingObject
{

    public int pointsPerFood = 10;                      // Food items' point values
    public int pointsPerSoda = 20;                      // Food items' point values
    public int wallDamage = 1;                          // How much damage chop inflicts to walls
    public int food;                                    // Player's food score value
    public Text foodText;                               // Food score text

    public float restartLevelDelay = 1f;                // Delay between levels

    private Animator animator;                          // Animator


    // Start is called before the first frame update.
    // "protected override", because we're overriding MovingObject's Start()
    protected override void Start()
    {
        animator = GetComponent<Animator>();

        // Get the player's current food points
        food = GameManager.instance.playerFoodPoints;
        foodText.text = "Food: " + food;

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
        // If it's NOT the player's turn, quit
        if (!GameManager.instance.playersTurn) return;

        // Where we're moving to, 1 or -1
        int horizontal = 0;
        int vertical = 0;

        horizontal = (int) Input.GetAxisRaw("Horizontal");
        vertical = (int) Input.GetAxisRaw("Vertical");

        // If the player moves horizontally, don't move vertically (diagonally)
        if (horizontal != 0)
            vertical = 0;

        // We're trying to move now!
        if (horizontal != 0 || vertical != 0)
            // We might hit a wall
            AttemptMove<Wall>(horizontal, vertical);
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        // Every move takes up 1 food
        food--;
        foodText.text = "Food: " + food;

        // Try to move, as per MovingObject
        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit;

        // Since food was lost -- is it game over already?
        CheckIfGameOver();

        // End of player's turn
        GameManager.instance.playersTurn = false;
    }

    // OnTriggerEnted2D() is part of Unity API
    // Check what kind of item the player collided with
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player found the exit!
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        // Player found food!
        else if (other.tag == "Food")
            {
            food += pointsPerFood;
            foodText.text = "+" + pointsPerFood + " Food: " + food;
            other.gameObject.SetActive(false);
        }
        // Player found a soda!
        else if (other.tag == "Soda")
            {
            food += pointsPerSoda;
            foodText.text = "+" + pointsPerSoda + " Food: " + food;
            other.gameObject.SetActive(false);
        }
    }

    // The player is trying to walk into a wall!
    protected override void OnCantMove<T>(T component)
    {
        // Let's store the passed component as the wall that was hit
        Wall hitWall = component as Wall;
        // The wall takes damage
        hitWall.DamageWall(wallDamage);
        // Player character chops (animation)
        animator.SetTrigger("playerChop");
    }

    // Player gets to exit!
    private void Restart()
    {
        // Restart level (advance per scripts)
        Application.LoadLevel(Application.loadedLevel);
    }

    // When an enemy hits player (loss = how many food points player loses)
    public void LoseFood (int loss)
    {
        // Animation: ouch!
        animator.SetTrigger("playerHit");
        // Substract player's food and check if s/he run out of food
        food -= loss;

        foodText.text = "-" + loss + " Food: " + food;
        CheckIfGameOver();
    }

    // Oooh, damn, did the player die?
    private void CheckIfGameOver()
    {
        if (food <= 0)
            GameManager.instance.GameOver();
    }
}
