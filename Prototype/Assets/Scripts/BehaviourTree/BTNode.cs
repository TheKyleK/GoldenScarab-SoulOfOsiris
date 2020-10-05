using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using System;
using UnityEngine.UIElements;

public enum BehaviourResult
{
    Success,
    Failure,
    Pending
}

[Serializable]
public abstract class BTNode
{
    public Port input;
    public float x = 0;
    public float y = 0;
    public BTNode()
    {
        //title = GetType().Name;
        //mainContainer.Clear();

        //mainContainer.style.flexDirection = FlexDirection.Column;
        //mainContainer.style.backgroundColor = new Color(0, 0, 0, 1);
        //mainContainer.style.unityTextAlign = TextAnchor.MiddleCenter;
        //mainContainer.style.alignContent = Align.Center;
        //mainContainer.style.alignSelf = Align.Center;


    }
    public abstract BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt);
    //public abstract BehaviourResult DebugLog(GameObject agent, Blackboard blackboard, float dt);
    //{
    //    BehaviourResult result = Execute(agent, blackboard, dt);
    //    if (result == BehaviourResult.Success)
    //    {
    //        if (!blackboard.Contains(BlackboardKey.Debug))
    //        {
    //            blackboard.Set(BlackboardKey.Debug, "");
    //        }

    //        string className = GetType().Name;
    //        blackboard.Set(BlackboardKey.Debug, blackboard.Get(BlackboardKey.Debug) + " " + className);
    //    }
    //}

    public virtual string GetPath(string input)
    {
        return input + " " + GetType().Name;
    }
}
