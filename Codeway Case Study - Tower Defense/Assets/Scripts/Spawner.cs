using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    public Action OnMonsterKilled;
    public Action OnAllMonstersKilled;

    [SerializeField] private Monster monsterPrefab;
    [SerializeField] private Transform monstersParent;
    [SerializeField] private Waypoint initialWaypoint;
    [SerializeField] private int spawnAmount;
    [SerializeField] private float spawnRate;

    private List<Monster> monsters = new List<Monster>();
    private int monstersAlive;

    public void SpawnMonsters(int stageCount)
    {
        CalculateSpawnAmount(stageCount);
        StartCoroutine(SpawnFromPool());
    }

    private IEnumerator SpawnFromPool()
    {
        foreach(Monster monster in monsters)
        {
            monster.gameObject.SetActive(true);
            monster.transform.position = monstersParent.position;
            monster.Initialize(initialWaypoint);
            monster.OnMonsterDeath += HandleDeadMonster;
            monstersAlive++;
            yield return new WaitForSeconds(spawnRate);
        }

        if (spawnAmount > monstersAlive)
        {
            int spawnCount = spawnAmount - monstersAlive;
            StartCoroutine(SpawnNew(spawnCount));
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
            monstersAlive--;
            monster.OnMonsterDeath -= HandleDeadMonster;
            monster.gameObject.SetActive(false);
            OnMonsterKilled?.Invoke();

            if(monstersAlive <= 0)
            {
                OnAllMonstersKilled?.Invoke();
            }
        }
    }

    private void CalculateSpawnAmount(int stageCount)
    {
        spawnAmount += (stageCount * 2);
    }
}
