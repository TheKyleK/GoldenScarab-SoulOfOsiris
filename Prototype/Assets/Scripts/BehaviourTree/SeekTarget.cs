using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekTarget : BTNode
{
    public CharacterRB rb;
    public BlackboardKey key;
    public float steeringForce;
    public SeekTarget(CharacterRB rb, BlackboardKey key, float steeringForce)
    {
        this.rb = rb;
        this.key = key;
        this.steeringForce = steeringForce;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (!blackboard.Contains(key)) return BehaviourResult.Failure;
        Vector3 target = blackboard.Get(key);
        Vector3 desireVelocity = (target - agent.transform.position).normalized * rb.speed;
        Vector3 force = (desireVelocity - rb.velocity) * steeringForce;
        rb.acceleration += force;
        return BehaviourResult.Success;
    }
}
