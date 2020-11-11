using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : TreeNode
{
    Vector3 m_position;
    public SetPosition(Vector3 position)
    {
        m_position = position;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        //Debug.Log(agent.transform.position);
        //Debug.Log(m_position);
        agent.SetActive(false);
        agent.transform.position = m_position;
        agent.SetActive(true);
        SoundManager.current.PlaySound(Sound.MonsterGrowl, agent.transform.position, 2);

        return BehaviourResult.Success;
    }
}
