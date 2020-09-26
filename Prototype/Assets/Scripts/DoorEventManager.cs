using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventManager : MonoBehaviour
{
    public List<GameObject> pedestals;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onPlayerPlaceDown += OnPlayerPlaceDown;
    }

    void OnPlayerPlaceDown(GameObject player, GameObject item)
    {
        bool allPlaced = true;
        foreach (GameObject pedestal in pedestals)
        {
            Inventory pedestalInventory = pedestal.GetComponent<Inventory>();
            if (pedestalInventory.items.Count == 0)
            {
                allPlaced = false;
                break;
            }
        }
        if (allPlaced)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        LeanTween.moveLocalY(gameObject, -2.55f, 1.0f).setEaseOutQuad().setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerPlaceDown -= OnPlayerPlaceDown;
    }

}
