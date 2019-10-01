using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=BXTjsCERdsw dit werkt misschien beter
public class GrapplingHook : MonoBehaviour
{
    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    public float maxDistance=10f;
    float distance;

    public LayerMask mask;
    public LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) /*&& joint.enabled == false*/)
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Detects where it hits a object
               hit = Physics2D.Raycast(transform.position, (targetPos - transform.position).normalized, maxDistance, mask);

            if (hit.collider != null)
            {
                // "Creates" the joint and fixes it to the right positions
                joint.enabled = true;
                joint.connectedAnchor = hit.point;
                joint.distance = Vector2.Distance(transform.position, hit.point);

                // "Creates" the line and fixes it to the right positions
                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);
            }
        }

        if (Input.GetMouseButtonDown(1) && joint.enabled == true)
        {
            joint.enabled = false;
            line.enabled = false;
            Debug.Log("joint disabled");
        }

        distance = Input.GetAxisRaw("Mouse ScrollWheel");

        joint.distance += distance * 2f;

        if (line.enabled == true)
        {
            line.SetPosition(0, transform.position);
        }

    }
}
