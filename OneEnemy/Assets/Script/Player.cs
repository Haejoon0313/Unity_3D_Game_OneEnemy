using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 5;

    bool dDown;
    bool isDodge = false;

    Vector3 destination = Vector3.zero;
    Vector3 moveVec;
    Vector3 dodgeVec;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Dodge();
    }

    void GetInput()
    {
        // right mouse input
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f))
            {
                destination = hit.point;

                
            }
        }
        destination.y = transform.position.y;

        // dodge key input
        dDown = Input.GetButtonDown("Dodge");
    }

    void Move()
    {
        if(Vector3.Distance(destination, transform.position) <= 0.01f)
        {
            anim.SetBool("isMove", false);
            return;
        }

        moveVec = (destination - transform.position).normalized;

        if (isDodge)
            moveVec = dodgeVec;
        
        transform.position += moveVec * speed * Time.deltaTime;
        anim.SetBool("isMove", true);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Dodge()
    {
        if (dDown && !isDodge && moveVec != Vector3.zero)
        {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 1f);
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }
}
