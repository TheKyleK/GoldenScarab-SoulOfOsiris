﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    // Objects
    public event Action<GameObject> onObjectTriggerEnter;
    public void ObjectTriggerEnter(GameObject obj)
    {
        onObjectTriggerEnter(obj);
    }

    public event Action<GameObject> onObjectTriggerExit;
    public void ObjectTriggerExit(GameObject obj)
    {
        onObjectTriggerExit(obj);
    }

    // Player
    public event Action<GameObject, GameObject> onPlayerRayHitEnter;
    public void PlayerRayHitEnter(GameObject player, GameObject target)
    {
        onPlayerRayHitEnter(player, target);
    }

    public event Action<GameObject> onPlayerRayHitExit;
    public void PlayerRayHitExit(GameObject player)
    {
        onPlayerRayHitExit(player);
    }


    //public event Action<GameObject, GameObject> onPlayerInteract;
    //public void PlayerInteract(GameObject player, GameObject target)
    //{
    //    onPlayerInteract(player, target);
    //}

    public event Action<GameObject, GameObject> onPlayerPickUp;
    public void PlayerPickUp(GameObject player, GameObject target)
    {
        onPlayerPickUp(player, target);
    }

    public event Action<GameObject, GameObject> onPlayerPlaceDown;
    public void PlayerPlaceDown(GameObject player, GameObject target)
    {
        onPlayerPlaceDown(player, target);
    }
}
