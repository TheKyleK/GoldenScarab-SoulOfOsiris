using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventManager2 : MonoBehaviour
{
    public List<TriggerEventManager> triggers;
    public float animationOffSet;
    public float doorAnimationTime;
    [Header("Camera shake")]
    public float magX;
    public float magY;
    public float time;
    public AnimationCurve curve;
    public GameObject sandParticles;
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
                EventManager.current.onTriggerActivated -= OnTriggerActivated;
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
        //SoundManager.current.PlaySound(Sound.DoorOpen, transform.position, 2);
        //if（sandParticles)
        //{
        //    sandParticles.SetActive(true);
        //}
        //LeanTween.moveLocalY(sandParticles, -8, doorAnimationTime).setEaseOutQuad().setOnComplete(() =>
        //{
        //sandParticles.SetActive(false);
        //Destroy(gameObject);
        //});
        if (sandParticles)
        {
            sandParticles.SetActive(true);
        }
        LeanTween.moveLocalY(gameObject, animationOffSet, doorAnimationTime).setEaseOutQuad().setOnComplete(() =>
        {
            //sandParticles.SetActive(false);
            //Destroy(gameObject);
        });
    }

    private void OnDestroy()
    {
        EventManager.current.onTriggerActivated -= OnTriggerActivated;
    }
}
