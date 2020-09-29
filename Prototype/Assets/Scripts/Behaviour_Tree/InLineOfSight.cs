using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InLineOfSight : Action
{
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask obstacleMask;

    public override ActionResult Execute(GameObject agent, float dt, Blackboard blackboard)
    {
        List<Vector3> targetInRadius = blackboard.Get(BlackboardKey.Input);
        List<Vector3> visibleTargets = new List<Vector3>();
        Transform eyeTransform = blackboard.Get(BlackboardKey.EyeTransform);
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
        if (visibleTargets.Count > 0) return ActionResult.Success;
        return ActionResult.Failure;
    }
}
