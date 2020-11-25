using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerEventManager : MonoBehaviour
{
    public LayerMask ignoreMask;
    private float m_rayDistance = 100.0f;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckRayHit();
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject hitObject = hit.transform.gameObject;
            ObjectEventManager oem = hitObject.GetComponent<ObjectEventManager>();
            if (oem && oem.rayhit && oem.entered)
            {
                EventManager.current.PlayerInteractObject(gameObject, hitObject);
            }
        }
    }

    void CheckRayHit()
    {
        Camera cam = Camera.main;
        Debug.DrawRay(cam.transform.position, cam.transform.forward * m_rayDistance);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, m_rayDistance, ~ignoreMask))
        {
            EventManager.current.PlayerRayHitEnter(gameObject, hit.transform.gameObject);
            return;
        }
        EventManager.current.PlayerRayHitExit(gameObject);
    }
}
