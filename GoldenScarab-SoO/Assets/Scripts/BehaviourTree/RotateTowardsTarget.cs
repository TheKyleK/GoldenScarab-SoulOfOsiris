using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsTarget : TreeNode
{
    private BlackboardKey m_key;
    private float m_rotationSpeed;
    public RotateTowardsTarget(BlackboardKey key, float rotationSpeed)
    {
        m_key = key;
        m_rotationSpeed = rotationSpeed;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (!blackboard.Contains(m_key)) return BehaviourResult.Failure;
        Vector3 target = blackboard.Get(m_key);
        Vector3 targetDir = target - agent.transform.position;
        Vector3 newDir = Vector3.RotateTowards(agent.transform.forward, targetDir, m_rotationSpeed * dt, 0.0f);
        agent.transform.rotation = Quaternion.LookRotation(newDir);
        return BehaviourResult.Success;
    }
}
