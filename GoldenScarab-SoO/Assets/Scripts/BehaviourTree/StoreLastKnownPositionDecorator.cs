using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreLastKnownPositionDecorator : Decorator
{
    private BlackboardKey m_key;
    public StoreLastKnownPositionDecorator(TreeNode child) : base(child)
    {

    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        BehaviourResult result = m_child.Execute(agent, blackboard, dt);
        if (result == BehaviourResult.Success)
        {
            blackboard.LastKnownPosition = blackboard.Position;
            blackboard.HasLastKnownPosition = true;
        }
        return result;
    }
}
