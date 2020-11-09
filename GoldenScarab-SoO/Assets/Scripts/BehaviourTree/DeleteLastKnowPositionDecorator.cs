using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLastKnowPositionDecorator : Decorator
{
    private BlackboardKey m_key;
    private BehaviourResult m_behaviourResult;
    public DeleteLastKnowPositionDecorator(TreeNode child, BehaviourResult behaviourResult) : base(child)
    {
        m_behaviourResult = behaviourResult;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        BehaviourResult result = m_child.Execute(agent, blackboard, dt);
        if (result == m_behaviourResult)
        {
            blackboard.HasLastKnownPosition = false;
        }
        return result;
    }
}
