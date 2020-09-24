using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalController : MonoBehaviour
{
    public int id;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onPedestalHit += OnPedestalHit;
        GameEvents.current.onPedestalTriggerExit += OnPedestalTriggerExit;
    }

    public void OnPedestalHit(int id, GameObject gameObject)
    {
        if (id == this.id)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            if (Input.GetKey(KeyCode.E))
            {
                Inventory inventory = player.GetComponent<Inventory>();
                if (inventory.items.Count > 0)
                {
                    GameEvents.current.InteractPedestal(id, gameObject);
                }
            }
        }
    }

    public void OnPedestalTriggerExit(int id)
    {
        if (id == this.id)
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0.4339094f, 0));
        }
    }


    private void OnDestroy()
    {
        GameEvents.current.onPedestalHit -= OnPedestalHit;
        GameEvents.current.onPedestalTriggerExit -= OnPedestalTriggerExit;
    }
}
