using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSeekTime : TreeNode
{
    float m_seekTime;
    public CheckSeekTime(float seekTime)
    {
        m_seekTime = seekTime;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        //float elapsed = dt + blackboard.TimeSeeking;
        //blackboard.TimeSeeking = elapsed;
        if (blackboard.TimeSeeking >= m_seekTime) return BehaviourResult.Success;
        return BehaviourResult.Failure;
    }

}
