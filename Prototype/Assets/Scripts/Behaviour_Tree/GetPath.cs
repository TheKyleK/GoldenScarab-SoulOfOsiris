using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class GetPath : Action
{
    public override ActionResult Execute(GameObject agent, float dt, Blackboard blackboard)
    {
        NavMeshAgent navMeshAgent = agent.GetComponent<NavMeshAgent>();
        NavMeshPath path = new NavMeshPath();
        if (!blackboard.Contains(BlackboardKey.Input)) return ActionResult.Failure;
        Vector3 targetPos = blackboard.Get(BlackboardKey.Input);
        if (navMeshAgent.CalculatePath(targetPos, path))
        {
            List<Vector3> wayPoints = path.corners.ToList();
            blackboard.Set(BlackboardKey.Input, wayPoints);
            return ActionResult.Success;
        }
        return ActionResult.Failure;
    }
}
