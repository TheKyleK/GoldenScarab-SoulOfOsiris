using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTeleportEventManager : MonoBehaviour
{
    public TriggerEventManager trigger;
    public GameObject monster;

    void Start()
    {
        EventManager.current.onTriggerActivated += OnTriggerActivated;
    }

    void OnTriggerActivated(GameObject obj)
    {
        if (trigger.triggered)
        {
            monster.SetActive(false);
            monster.transform.position = transform.position;
            monster.SetActive(true);
        }
    }
}
