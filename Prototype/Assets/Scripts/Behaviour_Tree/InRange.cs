using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class InRange : Action
{
    public float radius;
    public LayerMask targetMask;
    public override ActionResult Execute(GameObject agent, float dt, Blackboard blackboard)
    {
        List<Vector3> targetsInRadius = Physics.OverlapSphere(agent.transform.position, radius, targetMask).Select(x => x.transform.position).ToList();
        blackboard.Set(BlackboardKey.Input, targetsInRadius);
        if (targetsInRadius.Count > 0) return ActionResult.Success;
        return ActionResult.Failure;
    }
}
