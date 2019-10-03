using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // rigidbody
    Rigidbody2D rb;

    // speed and movement of rigidbody
    public float speed = 40f;
    float move;
    public bool facingRight = true;

    // animator
    public Animator animator;

    // jump 
    Vector2 jump = new Vector2(0, 1);
    public float jumpForce = 10.0f;
    public bool isGrounded = true;


    // stats
    public int health = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // get input from player
        move = Input.GetAxis("Horizontal");

        // move rigidbody accordingly
        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        // check velocity and let player stay left after walking
        if (rb.velocity.x > 0 && !facingRight || rb.velocity.x < 0 && facingRight)
        {
            facingRight = !facingRight;
        }

        // check space for jump
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            // jump
            rb.AddForce(jump * jumpForce, ForceMode2D.Force);
        }

        // flip object to corresponding side
        if (facingRight == true)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("Speed", Mathf.Abs(move));

        // death function
        if (health < 1)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();         
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            health -= collision.gameObject.GetComponent<EnemyScript>().damage;
        }
    }
}
