using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StoreOutputDecorator : Decorator
{
    public BlackboardKey key;
    public override ActionResult Execute(GameObject agent, float dt, Blackboard blackboard)
    {
        ActionResult result = child.Execute(agent, dt, blackboard);
        if (result == ActionResult.Success)
        {
            blackboard.Set(key, blackboard.Get(BlackboardKey.Input));
        }
        return result;
    }
}
