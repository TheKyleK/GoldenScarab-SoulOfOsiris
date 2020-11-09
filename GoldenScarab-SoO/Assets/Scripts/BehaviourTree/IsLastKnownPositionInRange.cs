using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLastKnownPositionInRange : TreeNode
{
    private float m_range;

    public IsLastKnownPositionInRange(float range)
    { 
        m_range = range;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (!blackboard.HasLastKnownPosition) return BehaviourResult.Failure;
        Vector3 target = blackboard.LastKnownPosition;
        float dist = Vector3.Distance(agent.transform.position, target);
        if (dist <= m_range) return BehaviourResult.Success;
        return BehaviourResult.Failure;
    }
}
