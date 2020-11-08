using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlackboardKey
{
    Input,
    LastKnownPosition,
    Debug,
    Path,
    PathIndex,
    Delay
}

public class Blackboard
{
    private Dictionary<BlackboardKey, dynamic> m_entries = new Dictionary<BlackboardKey, dynamic>();

    public void Set<T>(BlackboardKey key, T value)
    {
        if (!Contains(key))
        {
            m_entries.Add(key, value);
        }
        else
        {
            m_entries[key] = value;
        }
    }

    public dynamic Get(BlackboardKey key)
    {
        return m_entries[key];
    }

    public bool Contains(BlackboardKey key)
    {
        return m_entries.ContainsKey(key);
    }

    public void Remove(BlackboardKey key)
    {
        if (Contains(key))
        {
            m_entries.Remove(key);
        }
    }

}
