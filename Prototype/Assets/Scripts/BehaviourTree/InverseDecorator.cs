using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class InverseDecorator : Decorator
{
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        BehaviourResult result = child.Execute(agent, blackboard, dt);
        if (result == BehaviourResult.Success) return BehaviourResult.Failure;
        if (result == BehaviourResult.Failure) return BehaviourResult.Success;
        return result;
    }
}
