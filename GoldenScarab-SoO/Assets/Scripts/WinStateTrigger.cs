    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateTrigger : MonoBehaviour
{
    public List<TriggerEventManager> triggers;
    //public float animationOffSet;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onTriggerActivated += OnTriggerActivated;
    }

    void OnTriggerActivated(GameObject obj)
    {
        bool allTriggered = true;
        foreach (TriggerEventManager trigger in triggers)
        {
            if (trigger.triggered == false)
            {
                allTriggered = false;
                break;
            }
        }

        if (allTriggered)
        {
            WinGame();
            //EventManager.current.onTriggerActivated -= OnTriggerActivated;
        }
    }

    void WinGame()
    {
        GameSceneManager.current.LoadScene(3, 1);
    }

    private void OnDestroy()
    {
        EventManager.current.onTriggerActivated -= OnTriggerActivated;
    }
}
