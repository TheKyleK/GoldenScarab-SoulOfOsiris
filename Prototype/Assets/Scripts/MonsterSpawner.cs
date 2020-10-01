using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onPlayerPlaceDown += OnPlayerPlaceDown;
    }

    void OnPlayerPlaceDown(GameObject player, GameObject item)
    {
        GameObject monster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerPlaceDown -= OnPlayerPlaceDown;

    }
}
