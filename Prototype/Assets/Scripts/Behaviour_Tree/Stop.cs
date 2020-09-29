using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stop : Action
{
    public override ActionResult Execute(GameObject agent, float dt, Blackboard blackboard)
    {
        CharacterRB rb = agent.GetComponent<CharacterRB>();
        rb.acceleration *= 0;
        rb.velocity *= 0;
        return ActionResult.Success;
    }
}
