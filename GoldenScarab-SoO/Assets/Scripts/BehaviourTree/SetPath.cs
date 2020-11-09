using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetPath : TreeNode
{
    List<Transform> m_path;
    int m_index;
    public SetPath(List<Transform> path, int index)
    {
        m_path = path;
        m_index = index;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        blackboard.Path = m_path.Select(x => x.transform.position).ToList();
        blackboard.PathIndex = m_index;
        return BehaviourResult.Success;
    }
}
