using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTargetsInLineOfSight : BTNode
{
    public Transform eyeTransform;
    public float viewAngle;
    public LayerMask obstacleMask;
    public GetTargetsInLineOfSight(Transform eyeTransform, float viewAngle, LayerMask obstacleMask)
    {
        this.eyeTransform = eyeTransform;
        this.viewAngle = viewAngle;
        this.obstacleMask = obstacleMask;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        List<Vector3> targetInRadius = blackboard.Get(BlackboardKey.Input);
        List<Vector3> visibleTargets = new List<Vector3>();
        for (int i = 0; i < targetInRadius.Count; i++)
        {
            Vector3 target = targetInRadius[i];

            Vector3 directionToTarget = (target - agent.transform.position).normalized;
            if (Vector3.Angle(agent.transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(agent.transform.position, target);

                if (!Physics.Raycast(eyeTransform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
        blackboard.Set(BlackboardKey.Input, visibleTargets);
        return BehaviourResult.Success;
    }
}
