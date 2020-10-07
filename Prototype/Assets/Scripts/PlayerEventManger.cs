using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        GameEvents.current.onPlayerPickUpFromPedestal += OnPlayerPickUpFromPedestal;
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
                    GameObject item = hit.transform.gameObject;
                    ItemEventManager iem = item.GetComponent<ItemEventManager>();
                    if (iem != null && iem.rayhit && iem.entered)
                    {
                        GameObject parent = hit.transform.parent.parent.gameObject;
                        if (parent.transform.CompareTag("Pedestal"))
                        {
                            GameEvents.current.PlayerPickUpFromPedestal(gameObject, hit.transform.gameObject, parent);
                        }
                        else
                        {
                            GameEvents.current.PlayerPickUp(gameObject, hit.transform.gameObject);
                        }
                    }
                }

                if (hit.transform.CompareTag("Pedestal"))
                {
                    GameObject pedestal = hit.transform.gameObject;
                    PedestalEventManager pem = pedestal.GetComponent<PedestalEventManager>();
                    if (pem != null && pem.entered && pem.rayhit)
                    {
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
    }

    void OnPlayerPickUp(GameObject player, GameObject item)
    {
        if (player == gameObject)
        {
            Inventory inventory = GetComponent<Inventory>();
            if (inventory.Pickup(item))
            {
                item.transform.SetParent(player.transform);
                item.SetActive(false);
            }
        }
    }

    void OnPlayerPickUpFromPedestal(GameObject player, GameObject item, GameObject pedestal)
    {
        if (player == gameObject)
        {
            Inventory playerInventory = GetComponent<Inventory>();
            Inventory pedestalInventory = pedestal.GetComponent<Inventory>();
            if (pedestalInventory.items.Contains(item))
            {
                if (pedestalInventory.Transfer(pedestalInventory.items.IndexOf(item), playerInventory))
                {
                    item.SetActive(false);
                    item.transform.SetParent(player.transform);
                    SoundManager.current.PlaySound(Sound.Chime, item.transform.position);
                }
            }
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
                item.transform.localRotation = Quaternion.identity;
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
