using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InRangeTarget : Action
{
    public BlackboardKey key;
    public float range;
    public override ActionResult Execute(GameObject agent, float dt, Blackboard blackboard)
    {
        if (!blackboard.Contains(key)) return ActionResult.Failure;
        if (Vector3.Distance(agent.transform.position, blackboard.Get(key)) <= range)
        {
            return ActionResult.Success;
        }
        return ActionResult.Failure;
    }
}
