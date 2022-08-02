using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    public Action<Monster> OnMonsterKilled;
    public Action OnAllMonstersKilled;

    [SerializeField] private Monster monsterPrefab;
    [SerializeField] private Transform monstersParent;
    [SerializeField] private Waypoint initialWaypoint;
    [SerializeField] private int spawnAmount;
    [SerializeField] private float spawnRate;

    private List<Monster> monsters = new List<Monster>();
    private int monstersKilled;

    public void SpawnMonsters(int stageCount)
    {
        CalculateSpawnAmount(stageCount);
        StartCoroutine(SpawnFromPool());
    }

    private IEnumerator SpawnFromPool()
    {
        foreach(Monster monster in monsters)
        {
            yield return new WaitForSeconds(spawnRate);
            monster.gameObject.SetActive(true);
            monster.transform.position = monstersParent.position;
            monster.Initialize(initialWaypoint);
            monster.OnMonsterDeath += HandleDeadMonster; 
        }

        if (spawnAmount > monsters.Count)
        {
            int spawnCount = spawnAmount - monsters.Count;
            StartCoroutine(SpawnNew(spawnCount));
        }
    }

    private IEnumerator SpawnNew(int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            yield return new WaitForSeconds(spawnRate);
            Monster spawnedMonster = Instantiate(monsterPrefab, monstersParent);
            spawnedMonster.Initialize(initialWaypoint);
            spawnedMonster.OnMonsterDeath += HandleDeadMonster;
            monsters.Add(spawnedMonster);
        }
    }

    private void HandleDeadMonster(Monster monster)
    {
        if(monster != null)
        {
            monstersKilled++;
            monster.OnMonsterDeath -= HandleDeadMonster;
            monster.gameObject.SetActive(false);
            OnMonsterKilled?.Invoke(monster);

            if(monstersKilled == spawnAmount)
            {
                monstersKilled = 0;
                OnAllMonstersKilled?.Invoke();
            }
        }
    }

    private void CalculateSpawnAmount(int stageCount)
    {
        spawnAmount += (stageCount * 2);
    }
}
