﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : TreeNode
{
    private CharacterRB m_rb;
    public Stop(CharacterRB rb)
    {
        m_rb = rb;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        m_rb.force *= 0;
        return BehaviourResult.Success;
    }
}