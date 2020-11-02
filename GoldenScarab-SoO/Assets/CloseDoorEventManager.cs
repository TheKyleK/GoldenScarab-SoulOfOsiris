using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorEventManager : MonoBehaviour
{
    public List<TriggerEventManager> triggers;
    public float animationOffSet;
    public Transform mesh;
    public Transform meshCollider;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onTriggerActivated += OnTriggerActivated;
    }

    void OnTriggerActivated(GameObject obj)
    {
        //bool allTriggered = true;
        foreach (TriggerEventManager trigger in triggers)
        {
            if (trigger.triggered == true)
            {
                MoveDoor();
                //break;
            }
        }

        //if (allTriggered)
        //{
        //    OpenDoor();
        //    //EventManager.current.onTriggerActivated -= OnTriggerActivated;
        //}
    }

    void MoveDoor()
    {
        meshCollider.localPosition = new Vector3(0, animationOffSet, 0);
        LeanTween.moveLocalY(mesh.gameObject, animationOffSet, 1.0f).setEaseOutQuad();
    }

    private void OnDestroy()
    {
        EventManager.current.onTriggerActivated -= OnTriggerActivated;
    }
}
