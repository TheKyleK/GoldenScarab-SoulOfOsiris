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


    private Vector3 m_velocity;
    private Vector3 m_acceleration;
    public float friction;
    public float maxSpeed;
    public CharacterMovement controller;
    [SerializeField]
    private float m_mag;

    void FixedUpdate()
    {
        m_velocity += m_acceleration * Time.fixedDeltaTime;
        ClampMovement();
        ApplyFriction();
        m_mag = m_velocity.magnitude;
        controller.Move(m_velocity * Time.fixedDeltaTime);
        m_acceleration *= 0;
    }

    void ClampMovement()
    {
        Vector3 clamp = new Vector3(m_velocity.x, 0, m_velocity.z);
        if (clamp.magnitude > maxSpeed)
        {
            clamp = clamp.normalized * maxSpeed;
            m_velocity.x = clamp.x;
            m_velocity.z = clamp.z;
        }
    }

    void ApplyFriction()
    {
        Vector3 moveAcceleration = new Vector3(m_acceleration.x, 0, m_acceleration.z);
        if (moveAcceleration.magnitude == 0)
        {
            float decay = Mathf.Pow(1 - friction, Time.fixedDeltaTime);
            m_velocity.x *= decay;
            m_velocity.z *= decay;
        }

        Vector3 moveVelocity = new Vector3(m_velocity.x, 0, m_velocity.z);
        if (moveVelocity.magnitude < 0.1f)
        {
            m_velocity.x = 0;
            m_velocity.z = 0;
        }
    }

    public void ApplyForce(Vector3 force)
    {
        m_acceleration += force;
    }

    public Vector3 GetVelocity()
    {
        return m_velocity;
    }

    public void SetVelocity(Vector3 velocity)
    {
        m_velocity = velocity;
    }
}

