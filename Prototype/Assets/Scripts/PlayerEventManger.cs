using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerEventManger : MonoBehaviour
{
    public float rayDistance;
    public LayerMask ignoreMask;
    public RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onPlayerPickUp += OnPlayerPickUp;
        GameEvents.current.onPlayerPlaceDown += OnPlayerPlaceDown;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRayHit();
        if (Input.GetKeyDown(KeyCode.E))
        {
            // if the ray hits

            if (hit.transform.gameObject)
            {
                // if we are trying to interact with item
                if (hit.transform.CompareTag("Item"))
                {
                    GameEvents.current.PlayerPickUp(gameObject, hit.transform.gameObject);
                }

                if (hit.transform.CompareTag("Pedestal"))
                {
                    GameObject pedestal = hit.transform.gameObject;
                    Inventory playerInventory = GetComponent<Inventory>();
                    Inventory pedestalInventory = pedestal.GetComponent<Inventory>();
                    if (playerInventory.CanTransfer(0, pedestalInventory))
                    {
                        GameEvents.current.PlayerPlaceDown(gameObject, pedestal);
                    }
                }
            }
        }
    }

    void OnPlayerPickUp(GameObject player, GameObject item)
    {
        if (player == gameObject)
        {
            Inventory inventory = GetComponent<Inventory>();
            inventory.Pickup(item);
        }
    }

    void OnPlayerPlaceDown(GameObject player, GameObject pedestal)
    {
        if (player == gameObject)
        {
            Inventory playerInventory = GetComponent<Inventory>();
            Inventory pedestalInventory = pedestal.GetComponent<Inventory>();
            if (playerInventory.Transfer(0, pedestalInventory))
            {
                GameObject item = pedestalInventory.items[pedestalInventory.items.Count - 1];
                item.SetActive(true);
                Transform anchorPoint = pedestal.GetComponent<PedestalEventManager>().ancherPoint;
                item.transform.SetParent(anchorPoint);
                item.transform.localPosition = Vector3.zero;
                SoundManager.current.PlaySound(Sound.Ding, item.transform.position);
            }
        }
    }

    void CheckRayHit()
    {
        Camera cam = Camera.main;
        Debug.DrawRay(cam.transform.position, cam.transform.forward * rayDistance);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, rayDistance, ~ignoreMask))
        {
            GameEvents.current.PlayerRayHitEnter(gameObject, hit.transform.gameObject);
        }
        else
        {
            GameEvents.current.PlayerRayHitExit(gameObject);
        }

    }

}
