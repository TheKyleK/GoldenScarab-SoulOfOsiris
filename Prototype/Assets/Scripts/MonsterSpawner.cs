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
        GameEvents.current.onPlayerPlaceDown += OnPlayerPlaceDown;
    }

    void OnPlayerPlaceDown(GameObject player, GameObject item)
    {
        GameObject monster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        monster.GetComponent<MonsterMovement>().player = player;
        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerPlaceDown -= OnPlayerPlaceDown;

    }
}
