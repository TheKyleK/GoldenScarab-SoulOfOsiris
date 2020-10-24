using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private float m_moveForce;

    PlayerState m_state;
    CharacterRB m_rb;
    Animator m_animator;

    void Start()
    {
        m_state = new PlayerIdleState();
        m_rb = GetComponent<CharacterRB>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {

        PlayerState newState = m_state.HandleInput();
        if (newState != null)
        {
            m_state = newState;
        }
        m_state.UpdateMovement(Time.deltaTime, gameObject, m_rb, m_moveForce);
        m_state.UpdateAnimation(m_animator);
    }

    public void SetMoveForce(float moveForce)
    {
        m_moveForce = moveForce;
    }
}
