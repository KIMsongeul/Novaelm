using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 2f;
    public Vector3 movement = new Vector3();

    private Rigidbody rigid;
    private Animator anim;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        movement.Normalize();

        rigid.velocity = movement * speed;
        
        anim.SetFloat("VelocityX",movement.x);
        anim.SetFloat("VelocityZ",movement.z);

        if (movement.magnitude != 0)
        {
            anim.SetBool("Move",true);
        }
        else
        {
            anim.SetBool("Move",false);
        }
    }
}
