using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTargetInRange : TreeNode
{
    private BlackboardKey m_key;
    private float m_range;

    public IsTargetInRange(BlackboardKey key, float range)
    {
        m_key = key;
        m_range = range;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (!blackboard.Contains(m_key)) return BehaviourResult.Failure;
        Vector3 target = blackboard.Get(m_key);
        float dist = Vector3.Distance(agent.transform.position, target);
        if (dist <= m_range) return BehaviourResult.Success;
        return BehaviourResult.Failure;
    }
}
