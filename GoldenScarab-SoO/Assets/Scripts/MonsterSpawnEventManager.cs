using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnEventManager : MonoBehaviour
{
    public List<TriggerEventManager> triggers;
    public GameObject monsterPrefab;
    public MonsterMasterController characterMasterController;
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
        GameObject monster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        CharacterRB rb = monster.GetComponent<CharacterRB>();
        MonsterBehaviour mb = monster.GetComponent<MonsterBehaviour>();
        rb.characterMasterController = characterMasterController;
        mb.monsterMasterController = characterMasterController;
        //EventManager.current.MonsterSpawn(monster);
    }

    private void OnDestroy()
    {
        EventManager.current.onTriggerActivated -= OnTriggerActivated;
    }
}
