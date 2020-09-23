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

    // Itmes
    public event Action<int, GameObject> onItemHit;
    public void ItemHit(int id, GameObject gameObject)
    {
        if (onItemHit != null)
        {
            onItemHit(id, gameObject);
        }
    }

    public event Action<int> onItemTriggerExit;
    public void ItemTriggerExit(int id)
    {
        if (onItemTriggerExit != null)
        {
            onItemTriggerExit(id);
        }
    }

    // Pedestal
    public event Action<int, GameObject> onPedestalHit;
    public void PedestalHit(int id, GameObject gameObject)
    {
        if (onPedestalHit != null)
        {
            onPedestalHit(id, gameObject);
        }
    }

    public event Action<int> onPedestalTriggerExit;
    public void PedestalTriggerExit(int id)
    {
        if (onPedestalTriggerExit != null)
        {
            onPedestalTriggerExit(id);
        }
    }

    public event Action<int, GameObject> onInteractPedestal;
    public void InteractPedestal(int id, GameObject gameObject)
    {
        if (onInteractPedestal != null)
        {
            onInteractPedestal(id, gameObject);
        }
    }

    // Door
    public event Action<int> onOpenDoor;
    public void OpenDoor(int id)
    {
        if (onOpenDoor != null)
        {
            onOpenDoor(id);
        }
    }
}
