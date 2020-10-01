using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreOutputDecorator : Decorator
{
    public BlackboardKey key;
    public StoreOutputDecorator(BlackboardKey key)
    {
        this.key = key;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        BehaviourResult result = child.Execute(agent, blackboard, dt);
        if (result == BehaviourResult.Success)
        {
            if (blackboard.Contains(BlackboardKey.Input))
            {
                blackboard.Set(key, blackboard.Get(BlackboardKey.Input));
            }
        }
        return result;
    }
}
