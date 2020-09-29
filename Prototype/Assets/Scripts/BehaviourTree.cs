using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    public Action root;
    protected Blackboard blackboard = new Blackboard();

}
