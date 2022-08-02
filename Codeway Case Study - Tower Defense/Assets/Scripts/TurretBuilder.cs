using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurretBuilder : MonoBehaviour
{
    [SerializeField] private Transform turretsParent;
    [SerializeField] private Transform parentTurretTiles;
    private List<Transform> turretTiles;

    private LevelManager levelManager;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        GetTurretTiles();
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.OnBuildTurret += Build;
    }

    private void GetTurretTiles()
    {
        turretTiles = parentTurretTiles.GetComponentsInChildren<Transform>().ToList();
        turretTiles.Remove(parentTurretTiles);
    }

    public void Build(Turret turret)
    {
        if(TilesAvailable())
        {
            int randomIndex = Random.Range(0, turretTiles.Count);
            Vector2 buildPosition = turretTiles[randomIndex].transform.position;
            Turret newTurret = Instantiate(turret, buildPosition, Quaternion.identity, turretsParent);
            turretTiles.RemoveAt(randomIndex);
        }
        else
        {
            levelManager.SendMessageToUI("Not Enough Space! Can Not Build More Turrets!");
        }
    }

    private bool TilesAvailable()
    {
        return turretTiles.Count > 0;
    }
}
