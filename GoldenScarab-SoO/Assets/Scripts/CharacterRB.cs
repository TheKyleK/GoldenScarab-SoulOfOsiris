using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterRB : MonoBehaviour
{

    /// <summary>
    /// public variables no hungarian notation
    /// private/protected variables hungarian notation
    /// </summary>


    public Vector3 velocity;
    public Vector3 acceleration;
    public float friction;
    public float maxSpeed;
    public CharacterMovement controller;

    void FixedUpdate()
    {
        velocity += acceleration * Time.fixedDeltaTime;
        ClampMovement();
        controller.Move(velocity * Time.fixedDeltaTime);
        ApplyFriction();
        acceleration *= 0;
    }

    void ClampMovement()
    {
        Vector3 clamp = new Vector3(velocity.x, 0, velocity.z);
        if (clamp.magnitude > maxSpeed)
        {
            clamp = clamp.normalized * maxSpeed;
            velocity.x = clamp.x;
            velocity.z = clamp.z;
        }
    }

    void ApplyFriction()
    {
        float decay = Mathf.Pow(1 - friction, Time.fixedDeltaTime);
        velocity.x *= decay;
        velocity.z *= decay;
    }
}

