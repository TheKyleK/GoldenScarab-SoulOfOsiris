using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Sequence : Composite
{
    public override ActionResult Execute(GameObject agent, float dt, Blackboard blackboard)
    {
        Action child = pendingChild;
        pendingChild = null;
        if (children.Count == 0) return ActionResult.Failure;
        if (child == null) child = children[0];
        int index = children.IndexOf(child);
        for (int i = index; i < children.Count; i++)
        {
            child = children[i];
            ActionResult result = child.Execute(agent, dt, blackboard);
            if (result == ActionResult.Failure) return ActionResult.Failure;
            if (result == ActionResult.Pending)
            {
                pendingChild = child;
                return ActionResult.Pending;
            }
        }
        return ActionResult.Success;
    }
}
