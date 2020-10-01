using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAnimation : BTNode
{
    public Animator animator;
    public string animationState;

    public UpdateAnimation(Animator animator, string animationState)
    {
        this.animator = animator;
        this.animationState = animationState;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        for (int i = 0; i < animator.parameters.Length; i++)
        {
            string param = animator.parameters[i].name;
            animator.SetBool(param, animationState.Equals(param));
        }
        return BehaviourResult.Success;
    }
}
