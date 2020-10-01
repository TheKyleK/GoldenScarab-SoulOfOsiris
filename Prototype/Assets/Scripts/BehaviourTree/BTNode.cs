using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourResult
{
    Success,
    Failure,
    Pending
}
public abstract class BTNode
{
    public abstract BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt);
}
