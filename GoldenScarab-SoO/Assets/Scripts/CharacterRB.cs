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


    [SerializeField] private Vector3 m_velocity;
    [SerializeField] private Vector3 m_acceleration;
    //public CharacterMasterController characterMasterController;
    public CharacterMovement controller;
    public float maxSpeed;
    [Range(0, 1)]
    public float horizontalDampingStop;
    [Range(0, 1)]
    public float horizontalDampingTurn;
    [Range(0, 1)]
    public float horizontalDampingBasic;

    //public CharacterController controller;
    [SerializeField]
    private float m_mag;

    //public float dirX;
    //public float dirZ;
    public Vector3 force;

    [Header("gravity")]
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    void FixedUpdate()
    {
        //ApplyGraivity();
        ApplyForce();
        m_velocity += m_acceleration * Time.fixedDeltaTime;
        ApplyFriction();
        ClampMovement();
        //Vector3 moveAcceleration = new Vector3(m_acceleration.x, 0, m_acceleration.z);
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
        if (moveAcceleration.magnitude == 0.0f)
        {
            float decay = Mathf.Pow(1 - horizontalDampingStop, Time.fixedDeltaTime * 10);
            m_velocity.x *= decay;
            m_velocity.z *= decay;
        }
        else
        {
            if (Mathf.Sign(m_velocity.x) != Mathf.Sign(moveAcceleration.x))
            {
                float decay = Mathf.Pow(1 - horizontalDampingTurn, Time.fixedDeltaTime * 10);
                m_velocity.x *= decay;
            }
            else
            {
                float decay = Mathf.Pow(1 - horizontalDampingBasic, Time.fixedDeltaTime * 10);
                m_velocity.x *= decay;
            }

            if (Mathf.Sign(m_velocity.z) != Mathf.Sign(moveAcceleration.z))
            {
                float decay = Mathf.Pow(1 - horizontalDampingTurn, Time.fixedDeltaTime * 10);
                m_velocity.z *= decay;
            }
            else
            {
                float decay = Mathf.Pow(1 - horizontalDampingBasic, Time.fixedDeltaTime * 10);
                m_velocity.z *= decay;
            }


        }

        //float decay = Mathf.Pow(1 - m_friction, Time.fixedDeltaTime);
        //m_velocity.x *= decay;
        //m_velocity.z *= decay;



    }

    void ApplyGraivity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && m_velocity.y < 0)
        {
            m_velocity.y = 0;
            m_acceleration.y = -2f;
        }
        else
        {
            m_acceleration.y = gravity;
        }
    }

    public void ApplyForce()
    {
        //Vector3 move = transform.forward * dirZ + transform.right * dirX;
        //Vector3 moveForce = move.normalized * moveForce;
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

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

    //public void SetHorizontalDampingStop(float horizontalDampingStop)
    //{
    //    m_horizontalDampingStop = horizontalDampingStop;
    //}
    //public void SetHorizontalDampingTurn(float horizontalDampingTurn)
    //{
    //    m_horizontalDampingTurn = horizontalDampingTurn;
    //}
    //public void SetHorizontalDampingBasic(float horizontalDampingBasic)
    //{
    //    m_horizontalDampingBasic = horizontalDampingBasic;
    //}

    //public void SetMaxSpeed(float maxSpeed)
    //{
    //    m_maxSpeed = maxSpeed;
    //}

    public float GetMag()
    {
        return m_mag;
    }
}

