using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BlackboardKey
{
    Input,
    Storage,
    Debug
}

[Serializable]
public class Blackboard
{
    [SerializeField]
    public Dictionary<BlackboardKey, dynamic> entries = new Dictionary<BlackboardKey, dynamic>();

    public void Set<T>(BlackboardKey key, T value)
    {
        if (!Contains(key))
        {
            entries.Add(key, value);
        }
        else
        {
            entries[key] = value;
        }
    }

    public dynamic Get(BlackboardKey key)
    {
        return entries[key];
    }

    public bool Contains(BlackboardKey key)
    {
        return entries.ContainsKey(key);
    }

    public void Remove(BlackboardKey key)
    {
        if (Contains(key))
        {
            entries.Remove(key);
        }
    }

}