using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public Action<Turret> OnBuildTurret;

    [SerializeField] private int startingCoin;

    private UIManager UIManager;
    private TurretBuilder turretBuilder;
    private Spawner spawner;

    private int stageCount = 0; //might need to start at 1 when game manager/save system is done
    private int coins;
    private int totalMonstersKilled = 0;

    private void Awake()
    {
        UIManager = FindObjectOfType<UIManager>();
        turretBuilder = FindObjectOfType<TurretBuilder>();
        spawner = FindObjectOfType<Spawner>();
        spawner.OnMonsterKilled += IncreaseKillCount;
        spawner.OnAllMonstersKilled += StartNextStage;

        coins = startingCoin;

        UIManager.UpdateStageText(stageCount);
        UIManager.UpdateCoinsText(coins);
        UIManager.UpdateKillCountText(totalMonstersKilled);
    }

    private void Start()
    {
        StartNextStage();
    }

    public void BuildTurret(Turret turret)
    {
        if(coins >= turret.GetCost())
        {
            bool buildSuccessful = turretBuilder.Build(turret);

            if (buildSuccessful)
            {
                coins -= turret.GetCost();
                UIManager.UpdateCoinsText(coins);
            }
            else SendMessageToUI("Not Enough Space! Can Not Build More Turrets!");
        }
        else SendMessageToUI("Not Enough Coins!");
    }

    private void StartNextStage()
    {
        stageCount++;
        UIManager.UpdateStageText(stageCount);

        //start save (if you can)

        StartCoroutine(SetNextStage());

        spawner.SpawnMonsters(stageCount);
    }

    private IEnumerator SetNextStage()
    {
        //wait for 1 seconds before next stage
        //can update UI and notify player here
        SendMessageToUI("Stage " + stageCount);
        yield return new WaitForSeconds(1f);

        //do things for next stage
    }

    private void IncreaseKillCount(Monster monster)
    {
        totalMonstersKilled++;
        coins += monster.GetCoinsValue();
        UIManager.UpdateKillCountText(totalMonstersKilled);
        UIManager.UpdateCoinsText(coins);
    }

    public void SendMessageToUI(String message)
    {
        UIManager.ShowMessage(message);
    }
}
