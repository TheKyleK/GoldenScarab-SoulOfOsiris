using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsTarget : BTNode
{
    public BlackboardKey key;
    public float rotationSpeed;
    public RotateTowardsTarget(BlackboardKey key, float rotationSpeed)
    {
        this.key = key;
        this.rotationSpeed = rotationSpeed;
    }
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        if (!blackboard.Contains(key)) return BehaviourResult.Failure;
        Vector3 target = blackboard.Get(key);
        Vector3 targetDir = target - agent.transform.position;
        Vector3 newDir = Vector3.RotateTowards(agent.transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0.0f);
        agent.transform.rotation = Quaternion.LookRotation(newDir);
        return BehaviourResult.Success;
    }
}
