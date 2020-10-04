using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 5;

    bool dDown;
    bool s1Down;
    bool isDodge = false;
    bool isMove = false;
    bool isAttack = false;

    Vector3 destination;
    Vector3 moveVec;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();

        destination = transform.position;
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Dodge();
        Attack();
    }

    void GetInput()
    {
        if (!isDodge && !isAttack)
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
        }
        

        // dodge key input
        dDown = Input.GetButtonDown("Dodge");

        // skill 1 key input
        s1Down = Input.GetButtonDown("Skill1");
    }

    void Move()
    {
        if(Vector3.Distance(destination, transform.position) <= 0.01f)
        {
            isMove = false;
            anim.SetBool("isMove", isMove);
            return;
        }

        moveVec = (destination - transform.position).normalized;
        
        transform.position += moveVec * speed * Time.deltaTime;
        isMove = true;
        anim.SetBool("isMove", isMove);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Dodge()
    {
        // when press dodge button & not doing dodge & if moving
        if (dDown && !isDodge && isMove && !isAttack)
        {
            // accelerate speed
            speed *= 2;

            // animation
            anim.SetTrigger("doDodge");
            isDodge = true;

            // set destination to far away
            destination = transform.position + 10 * moveVec;

            // player immortal
            gameObject.layer = 9;
            
            // invoke dodge finish
            Invoke("DodgeOut", 1f);
        }
    }

    void DodgeOut()
    {
        // normal speed
        speed *= 0.5f;
        isDodge = false;

        // stop current position
        destination = transform.position;

        // player immortal inactive
        gameObject.layer = 8;
    }

    void Attack()
    {
        if(s1Down && !isDodge && !isAttack)
        {
            // stop current position
            destination = transform.position;

            // animation
            anim.SetTrigger("doSlash_4to6");
            isAttack = true;

            // invoke dodge finish
            Invoke("AttackOut", 1f);
        }
    }

    void AttackOut()
    {
        isAttack = false;
    }
}
