using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Get the next way point in the navmesh
/// </summary>
public class GetNextWaypointFromLastKnownPosition : TreeNode
{
    private NavMeshAgent m_agent;
    private float m_threshold;

    public GetNextWaypointFromLastKnownPosition(NavMeshAgent navAgent, float threshold)
    {
        m_agent = navAgent;
        m_threshold = threshold;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (!blackboard.HasLastKnownPosition) return BehaviourResult.Failure;
        NavMeshPath path = new NavMeshPath();
        Vector3 targetPos = blackboard.LastKnownPosition;
        if (m_agent.CalculatePath(targetPos, path))
        {
            for (int i = 0; i < path.corners.Length; i++)
            {
                Vector3 target = path.corners[i];
                blackboard.Position = target;
                if ((target - agent.transform.position).magnitude > m_threshold)
                {
                    return BehaviourResult.Success;
                }
            }
        }
        else
        {
            return BehaviourResult.Failure;
        }
        return BehaviourResult.Success;
    }
}
