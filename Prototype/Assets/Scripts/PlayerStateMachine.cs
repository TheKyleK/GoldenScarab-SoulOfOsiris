using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public float moveStrength;
    public Transform leftFoot;
    public Transform rightFoot;
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

    public void PlayFootStepSoundLeft()
    {
        SoundManager.current.PlaySound(Sound.FootStep, leftFoot.position);
    }

    public void PlayFootStepSoundRight()
    {
        SoundManager.current.PlaySound(Sound.FootStep, rightFoot.position);
    }
}
