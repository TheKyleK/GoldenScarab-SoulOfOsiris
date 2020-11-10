using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSeekTimeDecorator : Decorator
{
    float m_seekTime;
    BehaviourResult m_result;
    public SetSeekTimeDecorator(TreeNode child, float seekTime, BehaviourResult result) : base(child)
    {
        m_seekTime = seekTime;
        m_result = result;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        BehaviourResult result = m_child.Execute(agent, blackboard, dt);
        if (result == m_result)
        {
            blackboard.TimeSeeking = m_seekTime;
        }
        return result;
    }
}
