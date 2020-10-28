using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreOutputDecorator : Decorator
{
    private BlackboardKey m_key;
    public StoreOutputDecorator(TreeNode child, BlackboardKey key) : base(child)
    {
        m_key = key;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        BehaviourResult result = m_child.Execute(agent, blackboard, dt);
        if (result == BehaviourResult.Success)
        {
            if (blackboard.Contains(BlackboardKey.Input))
            {
                blackboard.Set(m_key, blackboard.Get(BlackboardKey.Input));
            }
        }
        return result;
    }
}
