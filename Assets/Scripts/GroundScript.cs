using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public GameObject Player;
    public bool isTouching = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTouching = true;
        Player.GetComponent<PlayerScript>().isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouching = false;
        Player.GetComponent<PlayerScript>().isGrounded = false;
    }
}
