using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public CharacterMasterController characterMasterController;

    public PlayerState state;
    CharacterRB m_rb;
    Animator m_animator;

    void Start()
    {
        state = new PlayerIdleState();
        m_rb = GetComponent<CharacterRB>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {

        PlayerState newState = state.HandleInput();
        if (newState != null)
        {
            state = newState;
        }
        state.UpdateRBDir(gameObject, m_rb, characterMasterController.moveForce);
        state.UpdateAnimation(m_animator);
        
    }
}
