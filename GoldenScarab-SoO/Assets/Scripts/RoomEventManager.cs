using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomEventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool oneTime;
    public bool continuous;
    public bool directional;
    public GameObject player;
    public Vector3 dir;
    private CharacterRB m_playerRB;
    void Start()
    {
        m_playerRB = player.GetComponent<CharacterRB>();
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
        bool triggered = ActivateTrigger(obj);
        if (triggered)
        {
            EventManager.current.TriggerActivated(obj);
        }
        if (oneTime && triggered)
        {
            EventManager.current.onObjectTriggerEnter -= OnObjectTriggerEnter;
        }
        
    }

    void OnObjectTriggerStay(GameObject obj)
    {
        ActivateTrigger(obj);
    }

    void OnObjectTriggerExit(GameObject obj)
    {
        bool triggered = ActivateTrigger(obj);
        if (triggered)
        {
            EventManager.current.TriggerDeactivated(obj);
        }
    }

    bool ActivateTrigger(GameObject obj)
    {
        if (gameObject == obj)
        {
            // if the room trigger is not directional, activate the trigger
            if (!directional)
            {
                return true;
            }
            // otherwise check the direction
            else
            {
                float dotProd = Vector3.Dot(m_playerRB.GetVelocity(), dir);
                if (dotProd > 0)
                {
                    return true;
                }
            }
        }
        return false;
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
