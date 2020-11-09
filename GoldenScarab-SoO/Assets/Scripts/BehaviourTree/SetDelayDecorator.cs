using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDelayDecorator : Decorator
{
    float m_delay;
    BehaviourResult m_result;
    public SetDelayDecorator(TreeNode child, float delay, BehaviourResult result) : base(child)
    {
        m_delay = delay;
        m_result = result;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        BehaviourResult result = m_child.Execute(agent, blackboard, dt);
        if (result == m_result)
        {
            blackboard.Delay = m_delay;
        }
        return result;
    }
}
