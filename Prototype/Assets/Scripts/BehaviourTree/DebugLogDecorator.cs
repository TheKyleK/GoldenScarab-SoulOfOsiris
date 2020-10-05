using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogDecorator : Decorator
{
    public string label;
    public BehaviourResult expectedResult;
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        BehaviourResult result = child.Execute(agent, blackboard, dt);
        if (result == expectedResult)
        {
            Debug.Log(label);
        }
        return result;
    }
}
