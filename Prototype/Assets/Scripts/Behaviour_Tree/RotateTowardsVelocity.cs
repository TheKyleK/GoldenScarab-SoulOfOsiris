using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RotateTowardsVelocity : Action
{
    public override ActionResult Execute(GameObject agent, float dt, Blackboard blackboard)
    {
        CharacterRB rb = agent.GetComponent<CharacterRB>();
        float heading = Mathf.Atan2(rb.velocity.x, rb.velocity.z);
        agent.transform.localRotation = Quaternion.Euler(0, heading * Mathf.Rad2Deg, 0);
        return ActionResult.Success;
    }
}
