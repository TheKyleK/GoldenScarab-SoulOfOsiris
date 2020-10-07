using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.current.ObjectTriggerEnter(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        GameEvents.current.ObjectTriggerEnter(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        GameEvents.current.ObjectTriggerExit(gameObject);
    }
}
