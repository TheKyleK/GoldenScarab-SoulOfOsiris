using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEventManager : MonoBehaviour
{
    private bool m_entered;
    private bool m_rayhit;

    //[ColorUsage(true, true)]
    public Color color1;
    //[ColorUsage(true, true)]
    public Color color2;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onObjectTriggerEnter += OnObjectTriggerEnter;
        EventManager.current.onObjectTriggerExit += OnObjectTriggerExit;
        EventManager.current.onPlayerRayHitEnter += OnPlayerRayHitEnter;
        EventManager.current.onPlayerRayHitExit += OnPlayerRayHitExit;
        GetComponent<Renderer>().material.SetColor("_BaseColor", color1);
    }
    void OnObjectTriggerEnter(GameObject obj)
    {
        if (obj == gameObject)
        {
            m_entered = true;
        }
    }

    void OnObjectTriggerExit(GameObject obj)
    {
        if (obj == gameObject)
        {
            m_entered = false;
        }
    }

    void OnPlayerRayHitEnter(GameObject player, GameObject obj)
    {
        if (obj == gameObject)
        {
            m_rayhit = true;
        }
        else
        {
            m_rayhit = false;
        }
    }

    void OnPlayerRayHitExit(GameObject player)
    {
        m_rayhit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_entered && m_rayhit)
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
        EventManager.current.onObjectTriggerExit -= OnObjectTriggerExit;
        EventManager.current.onPlayerRayHitEnter -= OnPlayerRayHitEnter;
        EventManager.current.onPlayerRayHitExit -= OnPlayerRayHitExit;
    }
}
