using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// get the closest target from the input list
/// </summary>
public class GetClosest : TreeNode
{
    private BlackboardKey m_key;

    public GetClosest(BlackboardKey key)
    {
        m_key = key;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (!blackboard.Contains(m_key)) return BehaviourResult.Failure;
        List<Vector3> targets = blackboard.Get(m_key);
        if (targets.Count == 0) return BehaviourResult.Failure;
        float closest = float.MaxValue;
        foreach (Vector3 target in targets)
        {
            float dist = Vector3.Distance(agent.transform.position, target);
            if (dist < closest)
            {
                closest = dist;
                blackboard.Set(BlackboardKey.Input, target);
            }
        }
        return BehaviourResult.Success;
    }
}
