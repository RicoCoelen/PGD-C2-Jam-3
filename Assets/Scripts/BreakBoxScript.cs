using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBoxScript : MonoBehaviour
{
    public GameObject[] breakAbleCrates;

    public float bforce = 1f;
    protected Rigidbody2D rb;
    private int active = 0;
    float explosionStrength = 1000;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.velocity.magnitude > bforce && active == 0)
        {
            // make active
            active++;
            // create crate
            GameObject BrokenCrate = Instantiate(breakAbleCrates[Random.Range(0, breakAbleCrates.Length)], transform.position, transform.rotation);
            // add force on each piece

            /* fix explosion force on box to make it more real
            for (int i = 0; i < BrokenCrate.transform.childCount; i++)
            {
                BrokenCrate.transform.GetChild(i).GetComponent<Rigidbody2D>().AddExplosionForce(explosionStrength, transform.position, 100000);
            }
            */

            // destroy current object
            Destroy(gameObject);
        }
    }
}