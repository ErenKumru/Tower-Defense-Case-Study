using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Monster monsterPrefab;
    [SerializeField] private Transform monstersParent;
    [SerializeField] private Waypoint initialWaypoint;
    [SerializeField] private int spawnCount;
    [SerializeField] private float spawnRate;

    private List<Monster> monsters = new List<Monster>();
    private int monstersAlive;

    private void Start()
    {
        SpawnMonsters();
    }

    private void SpawnMonsters()
    {
        StartCoroutine(SpawnFromPool());

        if (spawnCount > monstersAlive)
        {
            int spawnAmount = spawnCount - monstersAlive;
            StartCoroutine(SpawnNew(spawnAmount));
        }
    }

    private IEnumerator SpawnFromPool()
    {
        foreach(Monster monster in monsters)
        {
            monster.gameObject.SetActive(true);
            monster.transform.position = monstersParent.position;
            monster.OnMonsterDeath += HandleDeadMonster;
            monstersAlive++;
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private IEnumerator SpawnNew(int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Monster spawnedMonster = Instantiate(monsterPrefab, monstersParent);
            spawnedMonster.Initialize(initialWaypoint);
            spawnedMonster.OnMonsterDeath += HandleDeadMonster;
            monsters.Add(spawnedMonster);
            monstersAlive++;
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void HandleDeadMonster(Monster monster)
    {
        if(monster != null)
        {
            monster.OnMonsterDeath -= HandleDeadMonster;
            monster.gameObject.SetActive(false);
        }
    }
}
