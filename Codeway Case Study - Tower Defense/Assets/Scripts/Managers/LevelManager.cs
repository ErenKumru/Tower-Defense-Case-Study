using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public Action<Turret> OnBuildTurret;

    private int stageCount;
    private int coins;
    private int monstersDestroyed;

    public void BuildTurret(Turret turret)
    {
        if(coins >= turret.GetCost())
        {
            OnBuildTurret?.Invoke(turret);
        }
        else
        {
            SendMessageToUI("Not Enough Coins!");
        }
    }

    public void SendMessageToUI(String message)
    {
        //invoke event to UIManager show message
    }
}
