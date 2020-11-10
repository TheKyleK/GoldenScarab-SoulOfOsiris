using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCooldownDecorator : Decorator
{
    BehaviourResult m_result;
    float m_cooldown;

    public SetCooldownDecorator(TreeNode child, float cooldown, BehaviourResult result) : base(child)
    {
        m_result = result;
        m_cooldown = cooldown;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        BehaviourResult result = m_child.Execute(agent, blackboard, dt);
        if (result == m_result)
        {
            blackboard.Cooldown = m_cooldown;
        }
        return result;
    }
}
