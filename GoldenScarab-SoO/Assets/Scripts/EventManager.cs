using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    private void Awake()
    {
        current = this;
    }

    // Objects
    public event Action<GameObject> onObjectTriggerEnter;
    public void ObjectTriggerEnter(GameObject obj)
    {
        onObjectTriggerEnter?.Invoke(obj);
    }

    public event Action<GameObject> onObjectTriggerExit;
    public void ObjectTriggerExit(GameObject obj)
    {
        onObjectTriggerExit?.Invoke(obj);
    }
}
