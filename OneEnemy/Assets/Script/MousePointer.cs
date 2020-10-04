using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    Vector3 destination;
    MeshRenderer ren;

    void Awake()
    {
        ren = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        // right mouse input
        if (Input.GetMouseButtonDown(1))
        {
            CancelInvoke("Inactive");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f))
            {
                destination = hit.point;
                destination.y = 0.5f;
            }

            ren.enabled = true;
            transform.position = destination;

            Invoke("Inactive", 0.5f);
        }
        
        
    }
    void Inactive()
    {
        ren.enabled = false;
    }
}
