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

    // player raycast
    public event Action<GameObject, GameObject> onPlayerRayHitEnter;
    public void PlayerRayHitEnter(GameObject player, GameObject target)
    {
        onPlayerRayHitEnter?.Invoke(player, target);
    }

    public event Action<GameObject> onPlayerRayHitExit;
    public void PlayerRayHitExit(GameObject player)
    {
        onPlayerRayHitExit?.Invoke(player);
    }

    // player interact
    public event Action<GameObject, GameObject> onPlayerInteractObject;
    public void PlayerInteractObject(GameObject player, GameObject obj)
    {
        onPlayerInteractObject?.Invoke(player, obj);
    }

    // triggers
    public event Action<GameObject> onTriggerActivated;
    public void TriggerActivated(GameObject obj)
    {
        onTriggerActivated?.Invoke(obj);
    }

    public event Action<GameObject> onTriggerDeactivated;
    public void TriggerDeactivated(GameObject obj)
    {
        onTriggerDeactivated?.Invoke(obj);
    }

    // monster
    public event Action<GameObject> onMonsterSpawn;
    public void MonsterSpawn(GameObject obj)
    {
        onMonsterSpawn?.Invoke(obj);
    }

}
