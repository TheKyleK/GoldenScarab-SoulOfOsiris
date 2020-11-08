using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMemoryDecorator : Decorator
{
    private BlackboardKey m_key;
    private dynamic m_value;
    private BehaviourResult m_behaviourResult;
    public SetMemoryDecorator(TreeNode child, BlackboardKey key, dynamic value, BehaviourResult behaviourResult) : base(child)
    {
        m_key = key;
        m_value = value;
        m_behaviourResult = behaviourResult;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        BehaviourResult result = m_child.Execute(agent, blackboard, dt);
        if (result == m_behaviourResult)
        {
            blackboard.Set(m_key, m_value);
        }
        return result;
    }
}
