using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlackboardKey
{
    Position,
    Positions,
    LastKnownPosition,
    Path,
    PathIndex,
    Delay
}

public class Blackboard
{
    //private Dictionary<BlackboardKey, dynamic> m_entries = new Dictionary<BlackboardKey, dynamic>();
    //private Vector3 m_position;
    //private Vector3 m_lastKnownPosition;
    //private List<Transform> m_path;
    //private float m_pathIndex;
    //private float m_delay;
    //public void Set<T>(BlackboardKey key, T value)
    //{
    //    switch (key)
    //    {
    //        case BlackboardKey.Position:
    //            m_position = value;
    //            break;
    //        case BlackboardKey.LastKnownPosition:
    //            break;
    //        case BlackboardKey.Path:
    //            break;
    //        case BlackboardKey.PathIndex:
    //            break;
    //        case BlackboardKey.Delay:
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //public dynamic Get(BlackboardKey key)
    //{
    //    switch (key)
    //    {
    //        case BlackboardKey.Position:
    //            return m_position;
    //        case BlackboardKey.LastKnownPosition:
    //            return m_lastKnownPosition;
    //        case BlackboardKey.Path:
    //            return m_path;
    //        case BlackboardKey.PathIndex:
    //            return m_pathIndex;
    //        case BlackboardKey.Delay:
    //            return m_delay;
    //        default:
    //            return null;
    //    }
    //    //return m_entries[key];
    //}

    public Vector3 Position { get; set; }
    public List<Vector3> Positions { get; set; }
    public bool HasLastKnownPosition { get; set; }
    public Vector3 LastKnownPosition { get; set; }
    public List<Vector3> Path { get; set; }
    public int PathIndex { get; set; }
    public float Delay { get; set; }
    public float TimeSeeking { get; set; }
    public float Cooldown { get; set; }
    //public bool Contains(BlackboardKey key)
    //{
    //    return m_entries.ContainsKey(key);
    //}

    //public void Remove(BlackboardKey key)
    //{
    //    switch (key)
    //    {
    //        case BlackboardKey.Position:
    //            Position = null;
    //            break;
    //        case BlackboardKey.Positions:
    //            Positions = null;
    //            break;
    //        case BlackboardKey.LastKnownPosition:
    //            LastKnownPosition = null;
    //            break;
    //        case BlackboardKey.Path:
    //            Path = null;
    //            break;
    //        case BlackboardKey.PathIndex:
    //            PathIndex = 0;
    //            break;
    //        case BlackboardKey.Delay:
    //            Delay = 0;
    //            break;
    //        default:
    //            break;
    //    }
    //}

}
