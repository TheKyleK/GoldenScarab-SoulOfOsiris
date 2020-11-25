using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactEventManager : MonoBehaviour
{
    public Transform anchorPoint;
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
                    SoundManager.current.PlaySound(Sound.PickUp, obj.transform.position, 2);
                    obj.transform.SetParent(anchorPoint);
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.localRotation = Quaternion.identity;
                    obj.transform.localScale = new Vector3(2, 2, 2);
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
