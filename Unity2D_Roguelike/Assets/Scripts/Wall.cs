using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Sprite dmgSprite;
    public int hp = 4;

    private SpriteRenderer spriteRenderer;

    // Awake is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Player can damage the wall
    public void DamageWall (int loss)
    {
        // Show the "wall damaged!" sprite and decrease wall's HP
        spriteRenderer.sprite = dmgSprite;
        hp -= loss;
        // Destroy (hide) the wall
        if (hp <= 0)
            gameObject.SetActive(false);
    }
}
