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

    Vector2 rayOffset = new Vector2(0, 0);
    float offset = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        // cast a ray down to check if on ground or not
        RaycastHit2D floorCheck = Physics2D.Raycast(rb.position + rayOffset, Vector2.down, 2f, layerMask);
        RaycastHit2D rightWallCheck = Physics2D.Raycast(rb.position, Vector2.right, 0.5f, layerMask);
        RaycastHit2D leftWallCheck = Physics2D.Raycast(rb.position, Vector2.left, 0.5f, layerMask);

        // change movement direction if the raycast doesn't hit anything
        if (floorCheck.collider == false)
        {
            facingRight *= -1;
        }

        // Check if hitting a wall
        if (rightWallCheck.collider == true)
        {
            facingRight *= -1;
            rayOffset.x = -offset;
        }

        if (leftWallCheck.collider == true)
        {
            facingRight *= -1;
            rayOffset.x = offset;
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
