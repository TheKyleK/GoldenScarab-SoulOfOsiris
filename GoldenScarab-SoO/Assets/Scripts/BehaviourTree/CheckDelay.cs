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
        float delay = dt + blackboard.Delay;
        blackboard.Delay = delay;
        //Debug.Log(Delay);
        if (delay < m_maxDelay) return BehaviourResult.Success;
        //blackboard.Delay = 0;
        return BehaviourResult.Failure;
    }
}
