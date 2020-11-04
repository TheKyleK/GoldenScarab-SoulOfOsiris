using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBlackboardKey : TreeNode
{
    BlackboardKey m_key;
    dynamic m_value;
    public SetBlackboardKey(BlackboardKey key, dynamic value)
    {
        m_key = key;
        m_value = value;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        blackboard.Set(m_key, m_value);
        return BehaviourResult.Success;
    }
}
