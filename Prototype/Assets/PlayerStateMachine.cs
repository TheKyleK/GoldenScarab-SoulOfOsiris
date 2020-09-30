using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public float moveStrength;
    PlayerState state;
    CharacterRB rb;
    Animator animator;
   
    void Start()
    {
        state = new PlayerIdleState();
        rb = GetComponent<CharacterRB>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerState newState = state.HandleInput();
        if (newState != null)
        {
            state = newState;
        }
        state.UpdateMovement(gameObject, rb, moveStrength);
        state.UpdateAnimation(animator);
    }
}
