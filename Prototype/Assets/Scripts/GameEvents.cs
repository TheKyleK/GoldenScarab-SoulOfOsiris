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
        onObjectTriggerEnter?.Invoke(obj);
    }

    public event Action<GameObject> onObjectTriggerExit;
    public void ObjectTriggerExit(GameObject obj)
    {
        onObjectTriggerExit?.Invoke(obj);
    }

    // when an object is triggered
    // for example: button pressed, or item placed on pedestal
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

    // Player
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


    //public event Action<GameObject, GameObject> onPlayerInteract;
    //public void PlayerInteract(GameObject player, GameObject target)
    //{
    //    onPlayerInteract(player, target);
    //}

    public event Action<GameObject, GameObject> onPlayerPickUp;
    public void PlayerPickUp(GameObject player, GameObject target)
    {
        onPlayerPickUp?.Invoke(player, target);
    }

    public event Action<GameObject, GameObject, GameObject> onPlayerPickUpFromPedestal;
    public void PlayerPickUpFromPedestal(GameObject player, GameObject target, GameObject pedestal)
    {
        onPlayerPickUpFromPedestal?.Invoke(player, target, pedestal);
    }

    public event Action<GameObject, GameObject> onPlayerPlaceDown;
    public void PlayerPlaceDown(GameObject player, GameObject target)
    {
        onPlayerPlaceDown?.Invoke(player, target);
    }

    // Velocity
    public event Action<GameObject> onVelocityChanged;
    public void VelocityChanged(GameObject character)
    {
        onVelocityChanged?.Invoke(character);
    }
}
