using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class SeekTarget : Action
{
    public BlackboardKey key;
    public float steeringForce;
    public override ActionResult Execute(GameObject agent, float dt, Blackboard blackboard)
    {
        NavMeshAgent navMeshAgent = agent.GetComponent<NavMeshAgent>();
        NavMeshPath path = new NavMeshPath();
        CharacterRB rb = agent.GetComponent<CharacterRB>();
        Debug.Log("Test");
        if (!blackboard.Contains(key)) return ActionResult.Failure;
        Vector3 targetPos = blackboard.Get(key);
        if (navMeshAgent.CalculatePath(targetPos, path))
        {
            for (int i = 0; i < path.corners.Length; i++)
            {
                Vector3 target = path.corners[i];
                if ((target - agent.transform.position).magnitude > 0.5)
                {
                    Vector3 desireVelocity = (target - agent.transform.position).normalized * rb.speed;
                    Vector3 force = (desireVelocity - rb.velocity) * steeringForce;

                    rb.acceleration += force;
                    break;
                }
            }
        }
        return ActionResult.Success;
    }
}
