using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public int capacity;
    public CharacterController cc;
    public float r0;
    public float r1;
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
    private void Update()
    {
        if (cc)
        {
            if (items.Count > 0)
            {
                cc.radius = r1;
            }
            else
            {
                cc.radius = r0;
            }
        }
    }
}
