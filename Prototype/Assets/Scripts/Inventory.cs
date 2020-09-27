using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public int capacity = 1;

    bool CanPickUp()
    {
        return items.Count < capacity;
    }

    bool Empty()
    {
        return items.Count == 0;
    }

    public bool CanTransfer(int index, Inventory other)
    {
        return other.CanPickUp() && items.Count > index;
    }

    public bool Pickup(GameObject item)
    {
        if (CanPickUp())
        {
            SoundManager.current.PlaySound(Sound.Chime, item.transform.position);
            items.Add(item);
            return true;
        }
        return false;
    }

    public bool Transfer(int index, Inventory other)
    {
        if (CanTransfer(index, other))
        {
            other.items.Add(items[index]);
            items.RemoveAt(index);
            return true;
        }
        return false;
    }
    //void Start()
    //{
    //    //GameEvents.current.onItemHit += OnItemHit;
    //    //GameEvents.current.onInteractPedestal += OnInteractPedestal;
    //}

    //void OnItemHit(int id, GameObject gameObject)
    //{
    //    if (Input.GetKeyDown(KeyCode.E) && items.Count < capacity)
    //    {
    //        SoundManager.current.PlaySound("Chime", gameObject.transform.position);
    //        items.Add(gameObject);
    //        gameObject.SetActive(false);
    //    }
    //}

    //void OnInteractPedestal(int id, GameObject gameObject)
    //{
    //    if (items.Count > 0)
    //    {
    //        GameObject item = items[0];
    //        item.SetActive(true);
    //        item.transform.SetParent(gameObject.transform);
    //        item.transform.localPosition = new Vector3(0, 0.5f, 0);
    //        item.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    //        items.RemoveAt(0);
    //    }


    //}
}

