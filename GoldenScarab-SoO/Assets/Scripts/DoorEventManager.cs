using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventManager : MonoBehaviour
{
    public List<TriggerEventManager> triggers;
    public float animationOffSet;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onTriggerActivated += OnTriggerActivated;
    }

    void OnTriggerActivated(GameObject obj)
    {
        bool allTriggered = true;
        foreach (TriggerEventManager trigger in triggers)
        {
            if (trigger.triggered == false)
            {
                allTriggered = false;
                break;
            }
        }

        if (allTriggered)
        {
            OpenDoor();
            EventManager.current.onTriggerActivated -= OnTriggerActivated;
        }
    }

    void OpenDoor()
    {
        LeanTween.moveLocalY(gameObject, animationOffSet, 1.0f).setEaseOutQuad().setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void OnDestroy()
    {
        EventManager.current.onTriggerActivated -= OnTriggerActivated;
    }
}
