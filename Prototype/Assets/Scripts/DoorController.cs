using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int id;
    public List<GameObject> pedestals;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onInteractPedestal += InteractPedestal;
        GameEvents.current.onOpenDoor += OpenDoor;
    }

    void InteractPedestal(int id, GameObject gameObject)
    {
        foreach(GameObject pedestal in pedestals)
        {
            if (pedestal.GetComponent<PedestalController>().id == id)
            {
                pedestals.Remove(pedestal);
                break;
            }
        }
        if (pedestals.Count == 0)
        {
            GameEvents.current.OpenDoor(id);
        }
    }

    void OpenDoor(int id)
    {
        LeanTween.moveLocalY(gameObject, -2.55f, 1.0f).setEaseOutQuad().setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void OnDestroy()
    {
        GameEvents.current.onInteractPedestal -= InteractPedestal;
        GameEvents.current.onOpenDoor -= OpenDoor;
    }

}
