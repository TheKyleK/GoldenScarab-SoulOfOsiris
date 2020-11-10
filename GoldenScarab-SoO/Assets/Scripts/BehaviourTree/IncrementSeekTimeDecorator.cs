using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementSeekTimeDecorator : Decorator
{
    BehaviourResult m_result;
    public IncrementSeekTimeDecorator(TreeNode child, BehaviourResult result) : base(child)
    {
        m_result = result;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        BehaviourResult result = m_child.Execute(agent, blackboard, dt);
        if (result == m_result)
        {
            blackboard.TimeSeeking = blackboard.TimeSeeking + dt;
        }
        return result;
    }
}
