using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetClosest : BTNode
{
    public BlackboardKey key;

    public GetClosest(BlackboardKey key)
    {
        this.key = key;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (!blackboard.Contains(key)) return BehaviourResult.Failure;
        List<Vector3> targets = blackboard.Get(key);
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
