using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseGame : TreeNode
{
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        GameSceneManager.current.LoadScene(2, 1);
        return BehaviourResult.Success;
    }
}
