using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sequence node return success when all of the children successes
/// </summary>
public class SequenceNode : CompositeNode
{
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        TreeNode child = m_pendingChild;
        m_pendingChild = null;
        if (m_children.Count == 0) return BehaviourResult.Failure;
        if (child == null) child = m_children[0];
        int index = m_children.IndexOf(child);
        for (int i = index; i < m_children.Count; i++)
        {
            BehaviourResult result = m_children[i].Execute(agent, blackboard, dt);
            if (result == BehaviourResult.Failure) return BehaviourResult.Failure;
            if (result == BehaviourResult.Pending)
            {
                m_pendingChild = child;
                return BehaviourResult.Pending;
            }
        }
        return BehaviourResult.Success;
    }
}
