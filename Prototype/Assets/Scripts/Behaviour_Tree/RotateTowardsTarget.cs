using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RotateTowardsTarget : Action
{
    public override ActionResult Execute(GameObject agent, float dt, Blackboard blackboard)
    {
        if (!blackboard.Contains(BlackboardKey.Input)) return ActionResult.Failure;
        Vector3 targetPos = blackboard.Get(BlackboardKey.Input);
        float heading = Mathf.Atan2(agent.transform.position.x - targetPos.x, agent.transform.position.z - targetPos.z);
        agent.transform.localRotation = Quaternion.Euler(0, heading * Mathf.Rad2Deg + 180, 0);
        return ActionResult.Success;
    }
}
