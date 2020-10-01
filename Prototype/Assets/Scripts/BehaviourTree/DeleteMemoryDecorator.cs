using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMemoryDecorator : Decorator
{
    public BlackboardKey key;
    public BehaviourResult BehaviourResult;
    public DeleteMemoryDecorator(BlackboardKey key, BehaviourResult BehaviourResult)
    {
        this.key = key;
        this.BehaviourResult = BehaviourResult;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        BehaviourResult result = child.Execute(agent, blackboard, dt);
        if (result == BehaviourResult)
        {
            blackboard.Remove(key);
        }
        return result;
    }
}
