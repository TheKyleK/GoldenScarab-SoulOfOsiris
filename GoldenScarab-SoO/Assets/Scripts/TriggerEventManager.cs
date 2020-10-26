using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventManager : MonoBehaviour
{
    //[HideInInspector]
    public bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onTriggerActivated += OnTriggerActivated;
        EventManager.current.onTriggerDeactivated += OnTriggerDeactivated;
    }

    void OnTriggerActivated(GameObject obj)
    {
        if (gameObject == obj)
        {
            triggered = true;
        }
    }

    void OnTriggerDeactivated(GameObject obj)
    {
        if (gameObject == obj)
        {
            triggered = false;
        }
    }

    private void OnDestroy()
    {
        EventManager.current.onTriggerActivated -= OnTriggerActivated;
        EventManager.current.onTriggerDeactivated -= OnTriggerDeactivated;
    }
}
