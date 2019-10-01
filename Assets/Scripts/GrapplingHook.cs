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
    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && joint.enabled == false)
        {
            Debug.LogWarning("Key pressed");

            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, maxDistance, mask);

            // For now grapple to anything, if not wanted add gameobject.getcomponent like hit.collider
            if (hit.collider != null)
            {
                joint.enabled = true;
                Debug.Log(hit.point);
                joint.connectedAnchor = hit.point;
                joint.distance = Vector2.Distance(transform.position, hit.point);
            }
        }
        if (Input.GetMouseButtonDown(1) && joint.enabled == true)
        {
            joint.enabled = false;
            Debug.Log("joint disabled");
        }
    }
}
