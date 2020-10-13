using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventManager : MonoBehaviour
{
    public List<GameObject> triggers;
    public float animationOffSet;
    // Start is called before the first frame update
    void Start()
    {
        //GameEvents.current.onPlayerPlaceDown += OnPlayerPlaceDown;
        GameEvents.current.onTriggerActivated += OnTriggerActivated;
    }

    void OnTriggerActivated(GameObject gameObject)
    {
        bool allTriggered = true;
        foreach (GameObject trigger in triggers)
        {
            TriggerEventManager tem = trigger.GetComponent<TriggerEventManager>();
            if (tem.triggered == false)
            {
                allTriggered = false;
                break;
            }
        }
        if (allTriggered)
        {
            OpenDoor();
        }
    }

    //void OnPlayerPlaceDown(GameObject player, GameObject item)
    //{
    //    bool allPlaced = true;
    //    foreach (GameObject pedestal in pedestals)
    //    {
    //        Inventory pedestalInventory = pedestal.GetComponent<Inventory>();
    //        if (pedestalInventory.items.Count == 0)
    //        {
    //            allPlaced = false;
    //            break;
    //        }
    //    }
    //    if (allPlaced)
    //    {
    //        OpenDoor();
    //    }
    //}

    void OpenDoor()
    {
        LeanTween.moveLocalY(gameObject, animationOffSet, 1.0f).setEaseOutQuad().setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void OnDestroy()
    {
        //GameEvents.current.onPlayerPlaceDown -= OnPlayerPlaceDown;
        GameEvents.current.onTriggerActivated -= OnTriggerActivated;

    }

}
