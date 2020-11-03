using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fail : TreeNode
{
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        return BehaviourResult.Failure;
    }
}
