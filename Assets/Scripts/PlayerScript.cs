using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public const float startHealth = 100;
    public const float speed = 10;
    float health;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = Vector2.zero;

        if (Input.GetKey("w"))
        {
            velocity.y += 1;
        }
        if (Input.GetKey("a"))
        {
            velocity.x -= 1;
        }
        if (Input.GetKey("s"))
        {
            velocity.y -= 1;
        }
        if (Input.GetKey("d"))
        {
            velocity.x += 1;
        }

        velocity.Normalize();
        velocity *= speed;
    }
}
