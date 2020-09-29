using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decorator : Action
{
    public Action child;
}
