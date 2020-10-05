using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using Unity;
using System;

public class EntryPoint : BTNode
{
    public BTNode child;
    //public EntryPoint()
    //{
    //    CreateLabel(mainContainer, 10, Color.gray, Color.black, title);
    //    CreateOutputPort(mainContainer, 5, Color.black, Color.white, "Output");
    //}
    public override BehaviourResult Execute(GameObject agent, Blackboard blackboard, float dt)
    {
        return child.Execute(agent, blackboard, dt);
    }
}
