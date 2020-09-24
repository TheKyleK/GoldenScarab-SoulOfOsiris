using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onItemHit += OnItemHit;
        GameEvents.current.onItemTriggerExit += OnItemTriggerExit;
        //GameEvents.current.onInteractPedestal += OnInteractPedestal;
        //GameEvents.current.onPedestalTriggerExit += OnPedestalTriggerExit;
    }

    void OnItemHit(int id, GameObject gameObject)
    {
        if (id == this.id)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }

    void OnItemTriggerExit(int id)
    {
        if (id == this.id)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
    }

    //void OnInteractPedestal(int id, GameObject gameObject)
    //{
    //    if (id == this.id)
    //    {
    //        GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    //    }
    //}

    //void OnPedestalTriggerExit(int id)
    //{
    //    if (id == this.id)
    //    {
    //        GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    //    }
    //}

    private void OnDestroy()
    {
        GameEvents.current.onItemHit -= OnItemHit;
        GameEvents.current.onItemTriggerExit -= OnItemTriggerExit;
        //GameEvents.current.onInteractPedestal -= OnPedestalTriggerExit;
    }
}
