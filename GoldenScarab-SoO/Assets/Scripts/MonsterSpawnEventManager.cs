using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnEventManager : MonoBehaviour
{
    public List<TriggerEventManager> triggers;
    public GameObject monsterPrefab;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onTriggerActivated += OnTriggerActivated;
    }

    void OnTriggerActivated(GameObject obj)
    {
        foreach (TriggerEventManager trigger in triggers)
        {
            if (trigger.triggered == true)
            {
                SpawnMonster();
                EventManager.current.onTriggerActivated -= OnTriggerActivated;
                break;
            }
        }
    }

    void SpawnMonster()
    {
        Instantiate(monsterPrefab, transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        EventManager.current.onTriggerActivated -= OnTriggerActivated;
    }
}
