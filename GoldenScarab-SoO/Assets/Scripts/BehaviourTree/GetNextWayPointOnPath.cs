using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNextWayPointOnPath : TreeNode
{
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (!blackboard.Contains(BlackboardKey.Path) || !blackboard.Contains(BlackboardKey.PathIndex)) return BehaviourResult.Failure;
        List<Transform> path = blackboard.Get(BlackboardKey.Path);
        int index = blackboard.Get(BlackboardKey.PathIndex);
        Vector3 targetPos = path[index].position;
        blackboard.Set(BlackboardKey.Input, targetPos);
        //Debug.Log((targetPos - agent.transform.position).magnitude);
        while ((targetPos - agent.transform.position).magnitude < 1.0f)
        {
            index = (index + 1) % path.Count;
            blackboard.Set(BlackboardKey.PathIndex, index);
            targetPos = path[index].position;
            blackboard.Set(BlackboardKey.Input, targetPos);
        }
        return BehaviourResult.Success;
    }
}
