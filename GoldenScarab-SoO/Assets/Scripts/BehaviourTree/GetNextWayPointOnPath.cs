using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class GetNextWayPointOnPath : TreeNode
{
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (blackboard.Path == null) return BehaviourResult.Failure;
        List<Vector3> path = blackboard.Path;
        int index = blackboard.PathIndex;
        Vector3 targetPos = path[index];
        blackboard.Position = targetPos;
        //blackboard.Set(BlackboardKey.Position, targetPos);
        //Debug.Log((targetPos - agent.transform.position).magnitude);
        while ((targetPos - agent.transform.position).magnitude < 1.0f)
        {
            index = (index + 1) % path.Count;
            blackboard.PathIndex = index;
            //blackboard.Set(BlackboardKey.PathIndex, index);
            targetPos = path[index];
            blackboard.Position = targetPos;
            //blackboard.Set(BlackboardKey.Position, targetPos);
        }
        return BehaviourResult.Success;
    }
}
