using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDelay : TreeNode
{
    float m_maxDelay;

    public CheckDelay(float maxDelay)
    {
        m_maxDelay = maxDelay;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (!blackboard.Contains(BlackboardKey.Delay)) return BehaviourResult.Failure;
        float delay = dt + blackboard.Get(BlackboardKey.Delay);
        blackboard.Set(BlackboardKey.Delay, delay);
        if (delay < m_maxDelay) return BehaviourResult.Success;
        return BehaviourResult.Failure;
    }
}
