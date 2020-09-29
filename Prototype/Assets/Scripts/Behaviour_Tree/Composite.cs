using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Composite : Action
{
    public List<Action> children;
    [HideInInspector]
    public Action pendingChild = null;
}
