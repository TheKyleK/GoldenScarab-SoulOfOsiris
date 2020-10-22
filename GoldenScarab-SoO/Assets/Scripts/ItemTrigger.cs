using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventManager.current.ObjectTriggerEnter(gameObject);
        SoundManager.current.PlaySound(Sound.Chime, transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        EventManager.current.ObjectTriggerEnter(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        EventManager.current.ObjectTriggerExit(gameObject);
    }
}
