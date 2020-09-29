using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionResult
{
    Success,
    Failure,
    Pending
}


public abstract class Action : ScriptableObject
{
    public abstract ActionResult Execute(GameObject agent, float dt, Blackboard blackboard);
}
