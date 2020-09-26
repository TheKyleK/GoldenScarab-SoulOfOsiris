using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEventManager : MonoBehaviour
{
    [HideInInspector]
    public bool entered = false;
    [HideInInspector]
    public bool rayhit = false;
    void Start()
    {
        GameEvents.current.onObjectTriggerEnter += OnObjectTriggerEnter;
        GameEvents.current.onObjectTriggerExit += OnObjectTriggerExit;
        GameEvents.current.onPlayerRayHitEnter += OnPlayerRayHitEnter;
        GameEvents.current.onPlayerRayHitExit += OnPlayerRayHitExit;
        //GameEvents.current.onPlayerInteract += OnPlayerInteract;
        //GameEvents.current.onPlayerPickUp += OnPlayerPickUp;
    }

    private void Update()
    {
        if (entered && rayhit)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
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


    void OnPlayerRayHitEnter(GameObject player, GameObject item)
    {
        if (item == gameObject)
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

    private void OnDestroy()
    {
        GameEvents.current.onObjectTriggerEnter -= OnObjectTriggerEnter;
        GameEvents.current.onObjectTriggerExit -= OnObjectTriggerExit;
        GameEvents.current.onPlayerRayHitEnter -= OnPlayerRayHitEnter;
        GameEvents.current.onPlayerRayHitExit -= OnPlayerRayHitExit;
        //GameEvents.current.onPlayerInteract -= OnPlayerInteract;
    }
}
