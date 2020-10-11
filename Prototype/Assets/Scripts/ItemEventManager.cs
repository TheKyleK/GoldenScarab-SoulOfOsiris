using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEventManager : MonoBehaviour
{
    [HideInInspector]
    public bool entered = false;
    [HideInInspector]
    public bool rayhit = false;

    public float intensitiy;
    void Start()
    {
        GameEvents.current.onObjectTriggerEnter += OnObjectTriggerEnter;
        GameEvents.current.onObjectTriggerExit += OnObjectTriggerExit;
        GameEvents.current.onPlayerRayHitEnter += OnPlayerRayHitEnter;
        GameEvents.current.onPlayerRayHitExit += OnPlayerRayHitExit;
        Color color = new Color(0, 0.7509283f, 5.992157f);
        GetComponent<Renderer>().material.SetColor("_Emission", color);
        //GameEvents.current.onPlayerInteract += OnPlayerInteract;
        //GameEvents.current.onPlayerPickUp += OnPlayerPickUp;
    }

    private void Update()
    {
        if (entered && rayhit)
        {
            //GetComponent<Renderer>().material.shader = Shader.Find("HDRenderPipeline/Lit");
            float factor = Mathf.Pow(2, intensitiy);
            Color color = new Color(5.992157f, 0, 0.05640277f);
            GetComponent<Renderer>().material.SetColor("_Emission", color);
        }
        else
        {
            //GetComponent<Renderer>().material.shader = Shader.Find("HDRenderPipeline/Lit");
            Color color = new Color(0, 0.7509283f, 5.992157f);
            GetComponent<Renderer>().material.SetColor("_Emission", color);
           
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
