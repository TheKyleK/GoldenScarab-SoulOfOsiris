using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterRB : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 acceleration;
    public float friction;
    public float speed;
    //public CharacterController controller;
    public CharacterMovement controller;

    [Header("gravity")]
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
   
    void FixedUpdate()
    {
        ApplyGraivity();
        velocity += acceleration * Time.fixedDeltaTime;
        ClampMoveSpeed();
        controller.Move(velocity * Time.fixedDeltaTime);
        ApplyFriction();
        acceleration *= 0;
    }

    void ApplyFriction()
    {
        float decay = Mathf.Pow(1 - friction, Time.fixedDeltaTime);
        velocity.x *= decay;
        velocity.z *= decay;
    }

    void ClampMoveSpeed()
    {
        Vector3 veloctiyXZ = new Vector3(velocity.x, 0, velocity.z);
        if (veloctiyXZ.magnitude > speed)
        {
            veloctiyXZ = veloctiyXZ.normalized;
            veloctiyXZ *= speed;
            velocity.x = veloctiyXZ.x;
            velocity.z = veloctiyXZ.z;
        }
    }

    void ApplyGraivity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
            acceleration.y = -2f;
        }
        else
        {
            acceleration.y = gravity;
        }
    }
   
}
