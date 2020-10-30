﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalEventManager : MonoBehaviour
{
    public Transform anchorPoint;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onPlayerInteractObject += OnPlayerInteractObject;
    }


    void OnPlayerInteractObject(GameObject player, GameObject obj)
    {
        if (gameObject == obj)
        {
            Inventory inventory = player.GetComponent<Inventory>();
            Inventory pedestalInventory = obj.GetComponent<Inventory>();
            if (inventory != null && pedestalInventory != null)
            {
                if (inventory.Transfer(0, pedestalInventory))
                {
                    GameObject item = pedestalInventory.items[pedestalInventory.items.Count - 1];
                    item.SetActive(true);
                    item.transform.SetParent(transform);
                    item.transform.position = anchorPoint.transform.position;
                    item.transform.localRotation = Quaternion.identity;
                    SoundManager.current.PlaySound(Sound.Ding, item.transform.position);
                    EventManager.current.onPlayerInteractObject -= OnPlayerInteractObject;
                    EventManager.current.TriggerActivated(gameObject);
                }
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.current.onPlayerInteractObject -= OnPlayerInteractObject;
    }
}