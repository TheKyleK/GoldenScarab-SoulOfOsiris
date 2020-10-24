using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMemoryDecorator : Decorator
{
    private BlackboardKey m_key;
    private BehaviourResult m_behaviourResult;
    public DeleteMemoryDecorator(TreeNode child, BlackboardKey key, BehaviourResult behaviourResult) : base(child)
    {
        m_key = key;
        m_behaviourResult = behaviourResult;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        BehaviourResult result = m_child.Execute(agent, blackboard, dt);
        if (result == m_behaviourResult)
        {
            blackboard.Remove(m_key);
        }
        return result;
    }
}
