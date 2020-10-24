using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Seek to target position
/// </summary>
public class SeekTarget : TreeNode
{
    private CharacterRB m_rb;
    private BlackboardKey m_key;
    private float m_steeringForce;
    public SeekTarget(CharacterRB rb, BlackboardKey key, float steeringForce)
    {
        m_rb = rb;
        m_key = key;
        m_steeringForce = steeringForce;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (!blackboard.Contains(m_key)) return BehaviourResult.Failure;
        Vector3 target = blackboard.Get(m_key);
        Vector3 desireVelocity = (target - agent.transform.position).normalized * m_steeringForce;
        Vector3 force = (desireVelocity - m_rb.GetVelocity());
        m_rb.ApplyForce(force);
        return BehaviourResult.Success;
    }
}
