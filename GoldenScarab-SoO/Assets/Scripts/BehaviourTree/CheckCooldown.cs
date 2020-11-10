using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCooldown : TreeNode
{
    float m_cooldown;
    public CheckCooldown(float cooldown)
    {
        m_cooldown = cooldown;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        float elapsed = dt + blackboard.Cooldown;
        blackboard.Cooldown = elapsed;
        if (elapsed < m_cooldown) return BehaviourResult.Success;
        blackboard.Cooldown = 0;
        blackboard.TimeSeeking = 0;
        return BehaviourResult.Failure;
    }
}
