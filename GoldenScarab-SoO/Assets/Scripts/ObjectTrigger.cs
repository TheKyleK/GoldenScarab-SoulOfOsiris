using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.current.ObjectTriggerEnter(gameObject);
        }
        //SoundManager.current.PlaySound(Sound.Chime, transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.current.ObjectTriggerStay(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.current.ObjectTriggerExit(gameObject);
        }
    }
}
