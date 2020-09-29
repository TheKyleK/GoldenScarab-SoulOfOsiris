using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GetClosest : Action
{
    public override ActionResult Execute(GameObject agent, float dt, Blackboard blackboard)
    {
        
        if (!blackboard.Contains(BlackboardKey.Input)) return ActionResult.Failure;

        List<Vector3> targets = blackboard.Get(BlackboardKey.Input);
        if (targets.Count == 0) return ActionResult.Failure;

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
        return ActionResult.Success;
    }
}
