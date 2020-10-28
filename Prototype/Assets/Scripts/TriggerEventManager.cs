using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventManager : MonoBehaviour
{
    [HideInInspector]
    public bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onTriggerActivated += OnTriggerActivated;
        GameEvents.current.onTriggerDeactivated += OnTriggerDeactivated;
    }

    void OnTriggerActivated(GameObject gameObject)
    {
        if (gameObject == this.gameObject)
        {
            triggered = true;
        }
    }

    void OnTriggerDeactivated(GameObject gameObject)
    {
        if (gameObject == this.gameObject)
        {
            triggered = false;
        }
    }


}
