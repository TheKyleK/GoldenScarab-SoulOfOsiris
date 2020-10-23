using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Get targets in range and store them in the blackboard
/// </summary>
public class GetTargetsInRange : TreeNode
{
    private LayerMask m_targetMask;
    private float m_range;

    public GetTargetsInRange(LayerMask targetMask, float range)
    {
        m_targetMask = targetMask;
        m_range = range;
    }

    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        List<Vector3> targetInViewRadius = Physics.OverlapSphere(agent.transform.position, m_range, m_targetMask).Select(x => x.transform.position).ToList();
        blackboard.Set(BlackboardKey.Input, targetInViewRadius);
        return BehaviourResult.Success;
    }
}
