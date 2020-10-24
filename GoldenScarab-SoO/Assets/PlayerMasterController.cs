﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMasterController : MonoBehaviour
{
    public GameObject player;

    [Header("Movement")]
    public float moveForce;
    public float maxSpeed;
    [Range(0, 1)]
    public float friction;

    private CharacterRB m_rb;
    private PlayerStateMachine m_stateMachine;

    private void Start()
    {
        m_rb = player.GetComponent<CharacterRB>();
        m_stateMachine = player.GetComponent<PlayerStateMachine>();
        SetValues();

    }

    private void Update()
    {
        SetValues();
    }


    private void SetValues()
    {
        m_stateMachine.SetMoveForce(moveForce);
        m_rb.SetFriction(friction);
        m_rb.SetMaxSpeed(maxSpeed);
    }
}
