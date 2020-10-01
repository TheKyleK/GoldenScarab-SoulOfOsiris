using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
{
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        BTNode child = pendingChild;
        pendingChild = null;
        if (children.Count == 0) return BehaviourResult.Failure;
        if (child == null) child = children[0];
        int index = children.IndexOf(child);
        for (int i = index; i < children.Count; i++)
        {
            BehaviourResult result = children[i].Execute(agent, blackboard, dt);
            if (result == BehaviourResult.Success) return BehaviourResult.Success;
            if (result == BehaviourResult.Pending)
            {
                pendingChild = child;
                return BehaviourResult.Pending;
            }
        }
        return BehaviourResult.Failure;
    }
}
