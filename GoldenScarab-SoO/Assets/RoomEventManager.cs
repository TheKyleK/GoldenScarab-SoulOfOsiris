using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool oneTime;
    public bool continuous;
    void Start()
    {
        EventManager.current.onObjectTriggerEnter += OnObjectTriggerEnter;

        if (!oneTime)
        {
            if (continuous)
            {
                EventManager.current.onObjectTriggerStay += OnObjectTriggerStay;
            }
            EventManager.current.onObjectTriggerExit += OnObjectTriggerExit;
        }
    }

    void OnObjectTriggerEnter(GameObject obj)
    {
        if (gameObject == obj)
        {
            EventManager.current.TriggerActivated(obj);
            if (oneTime)
            {
                EventManager.current.onObjectTriggerEnter -= OnObjectTriggerEnter;
            }
        }
    }

    void OnObjectTriggerStay(GameObject obj)
    {
        if (gameObject == obj)
        {
            EventManager.current.TriggerActivated(obj);
        }
    }

    void OnObjectTriggerExit(GameObject obj)
    {
        if (gameObject == obj)
        {
            EventManager.current.TriggerDeactivated(obj);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        EventManager.current.onObjectTriggerEnter -= OnObjectTriggerEnter;
        EventManager.current.onObjectTriggerStay -= OnObjectTriggerStay;
        EventManager.current.onObjectTriggerExit -= OnObjectTriggerExit;
    }
}
