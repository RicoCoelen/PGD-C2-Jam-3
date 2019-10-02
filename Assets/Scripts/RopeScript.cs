using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public GameObject node;
    public float nodeInterfall;

    public GameObject lastNode;
    bool playerConnected = false;


    public GameObject player;

    public Vector2 destination;
    private Vector3 target;
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        lastNode = transform.gameObject;

        target = new Vector3(destination.x, destination.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target - transform.position;

        if (direction.magnitude > 1)
        {
            //Makes the rope travel like it was thrown
            direction.Normalize();
            transform.position += direction * speed;

            //Instantiate nodes
            if (Vector2.Distance(player.transform.position, lastNode.transform.position) > nodeInterfall)
            {
                CreateNode();
            }
        } else
        {
            transform.position = target;


            if (!playerConnected)
            {
                playerConnected = true;

                lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
            }
        }
    }


    /// <summary>
    /// Instantiate a new node for the chain
    /// </summary>
    void CreateNode()
    {
        Vector2 position = player.transform.position - lastNode.transform.position;

        position.Normalize();
        position *= nodeInterfall;
        position += (Vector2)lastNode.transform.position;

        GameObject go = Instantiate(node, position, Quaternion.identity);

        go.transform.SetParent(transform);

        lastNode.GetComponent<HingeJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();

        lastNode = go;
    }
}
