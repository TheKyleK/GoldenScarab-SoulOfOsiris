using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnEventManager : MonoBehaviour
{
    public List<TriggerEventManager> triggers;
    public GameObject monster;
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
                //SpawnMonster()
                //;
                //monster.transform.position = transform.position;
                StartCoroutine(SpawnMonster());
            }
        }
    }

    IEnumerator SpawnMonster()
    {
        SoundManager.current.PlaySound(Sound.MonsterGrowl, monster.transform.position, 2);
        EventManager.current.onTriggerActivated -= OnTriggerActivated;
        yield return new WaitForSeconds(2);
        monster.SetActive(true);
    }

    //void SpawnMonster()
    //{
    //    //GameObject monster = Instantiate(monster, transform.position, Quaternion.identity);
    //    //CharacterRB rb = monster.GetComponent<CharacterRB>();
    //    //MonsterBehaviour mb = monster.GetComponent<MonsterBehaviour>();
    //    //rb.characterMasterController = characterMasterController;
    //    //mb.monsterMasterController = characterMasterController;
    //    //EventManager.current.MonsterSpawn(monster);
    //}

    private void OnDestroy()
    {
        EventManager.current.onTriggerActivated -= OnTriggerActivated;
    }
}
