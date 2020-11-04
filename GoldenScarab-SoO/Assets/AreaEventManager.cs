using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject monster;
    private MonsterBehaviour m_mb;
    private WayPoints m_waypoints;
    void Start()
    {
        EventManager.current.onTriggerActivated += OnTriggerActivated;
        //EventManager.current.onTriggerDeactivated += OnTriggerDeactivated;
        m_mb = monster.GetComponent<MonsterBehaviour>();
        m_waypoints = GetComponent<WayPoints>();
    }
    
    void OnTriggerActivated(GameObject obj)
    {
        //Debug.Log("a");
        if (gameObject == obj)
        {
            //Debug.Log("b");
            //SequenceNode sequenceBefore = new SequenceNode();
            SequenceNode sequence = new SequenceNode();
            SetBlackboardKey setPath = new SetBlackboardKey(BlackboardKey.Path, m_waypoints.transforms);
            SetBlackboardKey setPathIndex = new SetBlackboardKey(BlackboardKey.PathIndex, 0);
            sequence.Add(setPath);
            sequence.Add(setPathIndex);
            m_mb.EnqueueBefore(sequence);
        }
    }
}
