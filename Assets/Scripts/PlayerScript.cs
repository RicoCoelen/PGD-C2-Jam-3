﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // rigidbody
    Rigidbody2D rb;

    // speed and movement of rigidbody
    public float speed = 40f;
    float move;
    bool facingRight = true;

    // animator
    public Animator animator;

    // jump 
    Vector2 jump = new Vector2(0, 1);
    public float jumpForce = 10.0f;
    public bool isGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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
    }
}
