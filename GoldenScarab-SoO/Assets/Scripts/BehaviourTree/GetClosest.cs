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
        if (blackboard.Positions == null) return BehaviourResult.Failure;
        List<Vector3> targets = blackboard.Positions;
        if (targets.Count == 0) return BehaviourResult.Failure;
        float closest = float.MaxValue;
        foreach (Vector3 target in targets)
        {
            float dist = (target - agent.transform.position).sqrMagnitude;
            if (dist < closest)
            {
                closest = dist;
                blackboard.Position = target;
            }
        }
        return BehaviourResult.Success;
    }
}
