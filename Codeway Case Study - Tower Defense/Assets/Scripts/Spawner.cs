using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Monster monsterPrefab;
    [SerializeField] private Transform monstersParent;
    [SerializeField] private Waypoint initialWaypoint;
    [SerializeField] private int spawnCount;

    private List<Monster> monsters = new List<Monster>();
    private int monstersAlive;

    private void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            Monster spawnedMonster = Instantiate(monsterPrefab, monstersParent);
            spawnedMonster.Initialize(initialWaypoint);
            monsters.Add(spawnedMonster);
            monstersAlive++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
