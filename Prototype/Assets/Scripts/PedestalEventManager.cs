using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PedestalEventManager : MonoBehaviour
{
    //[HideInInspector]
    public bool entered = false;
    //[HideInInspector]
    public bool rayhit = false;
    public Transform ancherPoint;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onObjectTriggerEnter += OnObjectTriggerEnter;
        GameEvents.current.onObjectTriggerExit += OnObjectTriggerExit;
        GameEvents.current.onPlayerRayHitEnter += OnPlayerRayHitEnter;
        GameEvents.current.onPlayerRayHitExit += OnPlayerRayHitExit;
        //GameEvents.current.onPlayerPickUp += OnPlayerPickUp;
    }

    void OnObjectTriggerEnter(GameObject obj)
    {
        if (obj == gameObject)
        {
            entered = true;
        }
    }
    void OnObjectTriggerExit(GameObject obj)
    {
        if (obj == gameObject)
        {
            entered = false;
        }
    }

    void OnPlayerRayHitEnter(GameObject player, GameObject pedestal)
    {
        if (pedestal == gameObject)
        {
            rayhit = true;
        }
        else
        {
            rayhit = false;
        }
    }

    void OnPlayerRayHitExit(GameObject player)
    {
        rayhit = false;
    }

    void OnPlayerPickUp(GameObject player, GameObject item)
    {
        Inventory pedestalInventory = GetComponent<Inventory>();
        Inventory playerInventory = player.GetComponent<Inventory>();
        if (pedestalInventory.items.Contains(item))
        {
            item.SetActive(false);
            pedestalInventory.items.RemoveAt(pedestalInventory.items.IndexOf(item));
            SoundManager.current.PlaySound(Sound.Chime, item.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (entered && rayhit)
        {
            GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_BaseColor", new Color(1, 0.6231f, 0));
        }
    }

    private void OnDestroy()
    {
        GameEvents.current.onObjectTriggerEnter -= OnObjectTriggerEnter;
        GameEvents.current.onObjectTriggerExit -= OnObjectTriggerExit;
        GameEvents.current.onPlayerRayHitEnter -= OnPlayerRayHitEnter;
        GameEvents.current.onPlayerRayHitExit -= OnPlayerRayHitExit;
        //GameEvents.current.onPlayerInteract -= OnPlayerInteract;
    }

}
