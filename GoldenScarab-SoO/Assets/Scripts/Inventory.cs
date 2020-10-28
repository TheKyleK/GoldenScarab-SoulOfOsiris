using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public int capacity;
    bool CanPickUp()
    {
        return items.Count < capacity;
    }

    public bool CanTransfer(int index, Inventory other)
    {
        return other.CanPickUp() && items.Count > index;
    }

    public bool Pickup(GameObject item)
    {
        if (CanPickUp())
        {
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
}
