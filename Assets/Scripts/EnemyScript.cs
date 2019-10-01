using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sprite;

    public float speed = 2f;
    public int facingRight = 1;

    public int health = 5;
    public int damage = 1;

    int layerMask = ~(1 << 9); //Exclude layer 9, so raycast doesn't hit the enemy itself

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        // cast a ray down to check if on ground or not
        RaycastHit2D raycast = Physics2D.Raycast(rb.position, Vector2.down, 2f, layerMask);

        // change movement direction if the raycast doesn't hit anything
        if (raycast.collider == false)
        {
            facingRight *= -1;
        }

        if (facingRight == 1)
        {
            sprite.flipX = true;
        } else
        {
            sprite.flipX = false;
        }

        rb.velocity = new Vector2(speed * facingRight, rb.velocity.y);

    }
}
