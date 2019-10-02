using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHook : MonoBehaviour
{
    public GameObject hook;

    GameObject currentHook;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Vector2 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            currentHook = Instantiate(hook, transform.position, Quaternion.identity);

            currentHook.GetComponent<RopeScript>().destination = destination;


        }
    }
}
