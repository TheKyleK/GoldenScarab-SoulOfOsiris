using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMasterController : MonoBehaviour
{
    //public CharacterMovementData data;
    //public GameObject player;

    [Header("Movement")]
    public float moveForce;
    public float maxSpeed;
    [Range(0, 1)]
    public float horizontalDampingStop;
    [Range(0, 1)]
    public float horizontalDampingTurn;
    [Range(0, 1)]
    public float horizontalDampingBasic;

    //private CharacterRB m_rb;
    //private PlayerStateMachine m_stateMachine;

    //private void Start()
    //{
    //    m_rb = player.GetComponent<CharacterRB>();
    //    m_stateMachine = player.GetComponent<PlayerStateMachine>();
    //    SetValues();

    //}

    //private void Update()
    //{
    //    SetValues();
    //}


    //private void SetValues()
    //{
    //    m_stateMachine.SetMoveForce(moveForce);
    //    m_rb.SetHorizontalDampingStop(horizontalDampingStop);
    //    m_rb.SetHorizontalDampingTurn(horizontalDampingTurn);
    //    m_rb.SetHorizontalDampingBasic(horizontalDampingBasic);
    //    m_rb.SetMaxSpeed(maxSpeed);
    //}
}
