using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    public float rayDistance;
    public LayerMask ignoreMask;

    int id;
    // Start is called before the first frame update
    void Start()
    {
        id = gameObject.GetComponent<ItemController>().id;
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckRayHit();
    }

    private void OnTriggerStay(Collider other)
    {
        CheckRayHit();
    }

    private void OnTriggerExit(Collider other)
    {
        GameEvents.current.ItemTriggerExit(id);
    }

    void CheckRayHit()
    {
        Camera cam = Camera.main;
        Debug.DrawRay(cam.transform.position, cam.transform.forward * rayDistance);
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, rayDistance, ~ignoreMask))
        {
            if (hit.transform.CompareTag("Item") && id == hit.transform.GetComponent<ItemController>().id)
            {
                GameEvents.current.ItemHit(id, hit.transform.gameObject);
            }
            else
            {
                GameEvents.current.ItemTriggerExit(id);
            }
        }
        else
        {
            GameEvents.current.ItemTriggerExit(id);
        }
    }
}
