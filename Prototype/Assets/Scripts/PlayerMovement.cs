using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //public float moveSpeed = 12.0f;
    //public float acceleration = 5.0f;
    //public float gravity = -9.81f;
    //public float friction = 0.95f;


    //public PlayerState state = PlayerState.Idle;
    Animator animator;

    CharacterRB rb;
    public float force;
 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<CharacterRB>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        //if (isGrounded && velocity.y < 0)
        //{
        //    velocity.y = -2f;
        //}

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Vector3 move = transform.forward * z + transform.right * x;
            Vector3 moveForce = move.normalized * force;

            rb.acceleration += moveForce;
        }

        //velocity.y += gravity * Time.deltaTime;
    }
}
