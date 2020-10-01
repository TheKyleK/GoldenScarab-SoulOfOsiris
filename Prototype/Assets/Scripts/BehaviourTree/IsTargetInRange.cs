using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTargetInRange : BTNode
{
    public BlackboardKey key;
    public float range;

    public IsTargetInRange(BlackboardKey key, float range)
    {
        this.key = key;
        this.range = range;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (!blackboard.Contains(key)) return BehaviourResult.Failure;
        Vector3 target = blackboard.Get(key);
        float dist = Vector3.Distance(agent.transform.position, target);
        if (dist <= range) return BehaviourResult.Success;
        return BehaviourResult.Failure;
    }
}
