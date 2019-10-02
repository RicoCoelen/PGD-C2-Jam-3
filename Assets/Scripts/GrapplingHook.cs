using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=BXTjsCERdsw dit werkt misschien beter
public class GrapplingHook : MonoBehaviour
{
    SpringJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    float distance;
 

    public LayerMask mask;
    public LineRenderer line;
    public float scrollSpeed;
    public float maxDistance = 10f;
    public float flyForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<SpringJoint2D>();
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

            if (hit.collider != null && hit.rigidbody == null)
            {
                // "Creates" the joint and fixes it to the right positions
                joint.enabled = true;
                joint.connectedBody = null;
                joint.connectedAnchor = hit.point;
                joint.distance = Vector2.Distance(transform.position, hit.point);
                

                // "Creates" the line and fixes it to the right positions
                line.enabled = true;
            }
            else
            {
                if (hit.rigidbody != null)
                {
                    joint.connectedBody = hit.rigidbody;
                    joint.enabled = true;
                    joint.connectedAnchor = Vector2.zero;
                    joint.distance = Vector2.Distance(transform.position, joint.connectedBody.position);

                    // "Creates" the line and fixes it to the right positions
                    line.enabled = true;
                }
                else
                {
                    joint.connectedBody = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && joint.enabled == true)
        {
            removeJoints();
        }

        if (Input.GetMouseButtonDown(2) && joint.enabled == true && joint.connectedBody != false)
        {
            //Vector2 flyVector = (Vector2) transform.position - (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
            
            //joint.connectedBody.AddForceAtPosition(flyVector * flyForce, joint.transform.position);


            Rigidbody2D rb = joint.connectedBody;
            removeJoints();
            rb.AddForce(((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2) rb.transform.position).normalized * flyForce, ForceMode2D.Impulse);
        } 

        distance = Input.GetAxisRaw("Mouse ScrollWheel") * scrollSpeed;
        joint.distance -= distance;

        // draw lines and fix distance
        if (line.enabled == true)
        {
            // always set first pos
            line.SetPosition(0, transform.position);

            // check if rigid
            if (joint.connectedBody != null)
            {
                line.SetPosition(1, joint.connectedBody.position);
            }
            else
            {
                line.SetPosition(1, hit.point);
            }
        }
    }
    void removeJoints()
    {
        // remove joint
        joint.enabled = false;
        line.enabled = false;

        // remove rigidbody
        if (joint.connectedBody != null)
        {
            joint.connectedBody = null;
        }
    }
}
