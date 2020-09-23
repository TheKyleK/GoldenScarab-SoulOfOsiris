using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalTrigger : MonoBehaviour
{
    public float rayDistance;
    public LayerMask ignoreMask;

    int id;
    // Start is called before the first frame update
    void Start()
    {
        id = gameObject.GetComponent<PedestalController>().id;
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
        GameEvents.current.PedestalTriggerExit(id);
    }

    void CheckRayHit()
    {
        Camera cam = Camera.main;
        Debug.DrawRay(cam.transform.position, cam.transform.forward * rayDistance);
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, rayDistance, ~ignoreMask))
        {
            if (hit.transform.CompareTag("Pedestal") && id == hit.transform.GetComponent<PedestalController>().id)
            {
                GameEvents.current.PedestalHit(id, hit.transform.gameObject);
            }
            else
            {
                GameEvents.current.PedestalTriggerExit(id);
            }
        }
        else
        {
            GameEvents.current.PedestalTriggerExit(id);
        }
    }
}
