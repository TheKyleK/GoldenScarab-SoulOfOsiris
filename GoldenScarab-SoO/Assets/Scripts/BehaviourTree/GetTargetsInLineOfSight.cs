using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Get targets that is in line of sight from input list
/// </summary>
public class GetTargetsInLineOfSight : TreeNode
{
    private Transform m_eyeTransform;
    private float m_viewAngle;
    private LayerMask m_obstacleMask;
    public GetTargetsInLineOfSight(Transform eyeTransform, float viewAngle, LayerMask obstacleMask)
    {
        m_eyeTransform = eyeTransform;
        m_viewAngle = viewAngle;
        m_obstacleMask = obstacleMask;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        List<Vector3> targetInRadius = blackboard.Get(BlackboardKey.Input);
        List<Vector3> visibleTargets = new List<Vector3>();
        for (int i = 0; i < targetInRadius.Count; i++)
        {
            Vector3 target = targetInRadius[i];

            Vector3 directionToTarget = (target - agent.transform.position).normalized;
            if (Vector3.Angle(agent.transform.forward, directionToTarget) < m_viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(agent.transform.position, target);

                if (!Physics.Raycast(m_eyeTransform.position, directionToTarget, distanceToTarget, m_obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
        blackboard.Set(BlackboardKey.Input, visibleTargets);
        return BehaviourResult.Success;
    }
}
