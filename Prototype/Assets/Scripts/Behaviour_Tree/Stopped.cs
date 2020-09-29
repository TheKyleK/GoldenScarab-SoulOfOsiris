using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stopped : Action
{
    public override ActionResult Execute(GameObject agent, float dt, Blackboard blackboard)
    {
        CharacterRB rb = agent.GetComponent<CharacterRB>();
        if (rb.velocity.magnitude == 0) return ActionResult.Success;
        return ActionResult.Failure;
    }
}
