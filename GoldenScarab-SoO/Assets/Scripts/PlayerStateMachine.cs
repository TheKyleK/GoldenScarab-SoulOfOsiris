using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerStateMachine : MonoBehaviour
{
    public CharacterMasterController characterMasterController;

    public PlayerState state;

    [Header("Head Bob")]
    public float ampitude;
    public float frequency;
    public Vector3 originalPos;
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
        state.UpdateHeadBobbing(Camera.main, m_rb, ampitude, frequency, originalPos);
        state.UpdateAnimation(m_animator);
        
    }
}
