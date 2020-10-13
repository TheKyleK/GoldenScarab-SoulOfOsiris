using System;
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

    // when an object is triggered
    // for example: button pressed, or item placed on pedestal
    public event Action<GameObject> onTriggerActivated;
    public void TriggerActivated(GameObject obj)
    {
        onTriggerActivated(obj);
    }

    public event Action<GameObject> onTriggerDeactivated;
    public void TriggerDeactivated(GameObject obj)
    {
        onTriggerDeactivated(obj);
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

    public event Action<GameObject, GameObject, GameObject> onPlayerPickUpFromPedestal;
    public void PlayerPickUpFromPedestal(GameObject player, GameObject target, GameObject pedestal)
    {
        onPlayerPickUpFromPedestal(player, target, pedestal);
    }

    public event Action<GameObject, GameObject> onPlayerPlaceDown;
    public void PlayerPlaceDown(GameObject player, GameObject target)
    {
        onPlayerPlaceDown(player, target);
    }

    // Velocity
    public event Action<GameObject> onVelocityChanged;
    public void VelocityChanged(GameObject character)
    {
        onVelocityChanged(character);
    }
}
