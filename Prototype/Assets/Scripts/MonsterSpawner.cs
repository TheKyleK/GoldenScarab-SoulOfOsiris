using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onInteractPedestal += InteractPedestal;
    }

    void InteractPedestal(int id, GameObject gameObject)
    {
        GameObject monster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        monster.GetComponent<MonsterMovement>().player = player;
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        GameEvents.current.onInteractPedestal -= InteractPedestal;
    }
}
