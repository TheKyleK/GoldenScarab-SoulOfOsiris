using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : ActionNode
{
    public CharacterRB rb;
    public Stop(CharacterRB rb)
    {
        this.rb = rb;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        rb.velocity *= 0;
        return BehaviourResult.Success;
    }
}
