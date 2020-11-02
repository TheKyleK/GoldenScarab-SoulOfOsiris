using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEventManager : MonoBehaviour
{
    public bool entered;
    public bool rayhit;

    //[ColorUsage(true, true)]
    public Color color1;
    //[ColorUsage(true, true)]
    public Color color2;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onObjectTriggerEnter += OnObjectTriggerEnter;
        EventManager.current.onObjectTriggerStay += OnObjectTriggerStay;
        EventManager.current.onObjectTriggerExit += OnObjectTriggerExit;
        EventManager.current.onPlayerRayHitEnter += OnPlayerRayHitEnter;
        EventManager.current.onPlayerRayHitExit += OnPlayerRayHitExit;
        GetComponent<Renderer>().material.SetColor("_BaseColor", color1);
    }
    void OnObjectTriggerEnter(GameObject obj)
    {
        if (obj == gameObject)
        {
            entered = true;
        }
    }

    void OnObjectTriggerStay(GameObject obj)
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

    void OnPlayerRayHitEnter(GameObject player, GameObject obj)
    {
        if (obj == gameObject)
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

    // Update is called once per frame
    void Update()
    {
        if (entered && rayhit)
        {
            GetComponent<Renderer>().material.SetColor("_BaseColor", color2);
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_BaseColor", color1);

        }
    }

    private void OnDestroy()
    {
        EventManager.current.onObjectTriggerEnter -= OnObjectTriggerEnter;
        EventManager.current.onObjectTriggerStay -= OnObjectTriggerStay;
        EventManager.current.onObjectTriggerExit -= OnObjectTriggerExit;
        EventManager.current.onPlayerRayHitEnter -= OnPlayerRayHitEnter;
        EventManager.current.onPlayerRayHitExit -= OnPlayerRayHitExit;
    }
}
