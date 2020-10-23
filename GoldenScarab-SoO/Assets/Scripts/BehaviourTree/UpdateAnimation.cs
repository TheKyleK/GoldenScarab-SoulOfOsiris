using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAnimation : TreeNode
{
    private Animator m_animator;
    private string m_animationState;

    public UpdateAnimation(Animator animator, string animationState)
    {
        m_animator = animator;
        m_animationState = animationState;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        for (int i = 0; i < m_animator.parameters.Length; i++)
        {
            string param = m_animator.parameters[i].name;
            m_animator.SetBool(param, m_animationState.Equals(param));
        }
        return BehaviourResult.Success;
    }
}
