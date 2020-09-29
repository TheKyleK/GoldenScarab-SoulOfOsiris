using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InverseDecorator : Decorator
{
    public override ActionResult Execute(GameObject agent, float dt, Blackboard blackboard)
    {
        ActionResult result = child.Execute(agent, dt, blackboard);
        if (result == ActionResult.Success) return ActionResult.Failure;
        if (result == ActionResult.Failure) return ActionResult.Success;
        return result;
    }
}
