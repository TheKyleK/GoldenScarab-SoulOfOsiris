using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public int capacity = 1;
    void Start()
    {
        GameEvents.current.onItemHit += OnItemHit;
        GameEvents.current.onInteractPedestal += OnInteractPedestal;
    }

    void OnItemHit(int id, GameObject gameObject)
    {
        if (Input.GetKey(KeyCode.E) && items.Count < capacity)
        {
            items.Add(gameObject);
            gameObject.SetActive(false);
        }
    }

    void OnInteractPedestal(int id, GameObject gameObject)
    {
        if (items.Count > 0)
        {
            GameObject item = items[0];
            item.SetActive(true);
            item.transform.SetParent(gameObject.transform);
            item.transform.localPosition = new Vector3(0, 0.5f, 0);
            item.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            items.RemoveAt(0);
        }
    }
}

