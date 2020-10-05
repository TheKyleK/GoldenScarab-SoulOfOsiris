using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetTargetsInRange : ActionNode
{
    public LayerMask targetMask;
    public float range;

    public GetTargetsInRange(LayerMask targetMask, float range)
    {
        this.targetMask = targetMask;
        this.range = range;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        List<Vector3> targetInViewRadius = Physics.OverlapSphere(agent.transform.position, range, targetMask).Select(x => x.transform.position).ToList();
        blackboard.Set(BlackboardKey.Input, targetInViewRadius);
        return BehaviourResult.Success;
    }
}
