using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactEventManager : MonoBehaviour
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
            Inventory inventory = player.GetComponent<Inventory>();
            if (inventory != null)
            {
                if (inventory.Pickup(obj))
                {
                    SoundManager.current.PlaySound(Sound.Chime, obj.transform.position);
                    obj.SetActive(false);
                    EventManager.current.onPlayerInteractObject -= OnPlayerInteractObject;
                }
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.current.onPlayerInteractObject -= OnPlayerInteractObject;
    }
}
