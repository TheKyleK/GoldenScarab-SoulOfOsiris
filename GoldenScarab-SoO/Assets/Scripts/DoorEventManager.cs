using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventManager : MonoBehaviour
{
    public List<TriggerEventManager> triggers;
    public float animationOffSet;
    [Header("Camera shake")]
    public float magX;
    public float magY;
    public float time;
    public AnimationCurve curve;
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
                OpenDoor();
                //break;
            }
        }

        //if (allTriggered)
        //{
        //    OpenDoor();
        //    //EventManager.current.onTriggerActivated -= OnTriggerActivated;
        //}
    }

    void OpenDoor()
    {
        CameraShake.current.Shake(magX, magY, time, curve);
        LeanTween.moveLocalY(gameObject, animationOffSet, 1.0f).setEaseOutQuad().setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void OnDestroy()
    {
        EventManager.current.onTriggerActivated -= OnTriggerActivated;
    }
}
