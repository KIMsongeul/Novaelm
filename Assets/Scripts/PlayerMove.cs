using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    public float jumpPower = 55f;
    public Vector3 movement = new Vector3();
    
    [Header("Laycast")]
    public LayerMask groundLayer;

    private float gravity = 9f;
    private float groundCheckDistance = 0.1f;
    private bool isGround;

    private Rigidbody rigid;
    private Animator anim;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
            anim.SetTrigger("Jump");
        }

        if (rigid.velocity.y < 0)
        {
            rigid.AddForce(Vector3.down * gravity,ForceMode.Force);
            Debug.Log("falling fast");
        }
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

    void Jump()
    {
        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
}
