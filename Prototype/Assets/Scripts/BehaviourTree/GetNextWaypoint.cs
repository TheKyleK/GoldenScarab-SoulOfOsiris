using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GetNextWaypoint : BTNode
{
    public NavMeshAgent navAgent;
    public BlackboardKey key;
    public float threshold;

    public GetNextWaypoint(NavMeshAgent navAgent, BlackboardKey key, float threshold)
    {
        this.navAgent = navAgent;
        this.key = key;
        this.threshold = threshold;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (!blackboard.Contains(key)) return BehaviourResult.Failure;
        NavMeshPath path = new NavMeshPath();
        Vector3 targetPos = blackboard.Get(key);
        if (navAgent.CalculatePath(targetPos, path))
        {
            for (int i = 0; i < path.corners.Length; i++)
            {
                Vector3 target = path.corners[i];
                blackboard.Set(BlackboardKey.Input, target);
                if ((target - agent.transform.position).magnitude > threshold)
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
