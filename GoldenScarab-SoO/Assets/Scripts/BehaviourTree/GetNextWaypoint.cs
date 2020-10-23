using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Get the next way point in the navmesh
/// </summary>
public class GetNextWaypoint : TreeNode
{
    private NavMeshAgent m_agent;
    private BlackboardKey m_key;
    private float m_threshold;

    public GetNextWaypoint(NavMeshAgent navAgent, BlackboardKey key, float threshold)
    {
        m_agent = navAgent;
        m_key = key;
        m_threshold = threshold;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (!blackboard.Contains(m_key)) return BehaviourResult.Failure;
        NavMeshPath path = new NavMeshPath();
        Vector3 targetPos = blackboard.Get(m_key);
        if (m_agent.CalculatePath(targetPos, path))
        {
            for (int i = 0; i < path.corners.Length; i++)
            {
                Vector3 target = path.corners[i];
                blackboard.Set(BlackboardKey.Input, target);
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
