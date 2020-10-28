using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onPlayerInteractObject += OnPlayerInteractObject;
    }

    void OnPlayerInteractObject(GameObject player, GameObject obj)
    {
        if (gameObject == obj)
        {
            EventManager.current.TriggerActivated(gameObject);
            EventManager.current.onPlayerInteractObject -= OnPlayerInteractObject;
        }
    }

    private void OnDestroy()
    {
        EventManager.current.onPlayerInteractObject -= OnPlayerInteractObject;
    }
}
