using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : BehaviourTree
{
    //public Action root;
    public Transform eyeTransform;
    //[HideInInspector]
    //Blackboard blackboard = new Blackboard();
    private void Awake()
    {
        blackboard.Set(BlackboardKey.EyeTransform, eyeTransform);
    }
    void Update()
    {
        root.Execute(gameObject, Time.deltaTime, blackboard);
    }
}
