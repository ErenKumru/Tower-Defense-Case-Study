using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurretBuilder : MonoBehaviour
{
    [SerializeField] private Transform turretsParent;
    [SerializeField] private Transform parentTurretTiles;
    private List<Transform> turretTiles;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        GetTurretTiles();
    }

    private void GetTurretTiles()
    {
        turretTiles = parentTurretTiles.GetComponentsInChildren<Transform>().ToList();
        turretTiles.Remove(parentTurretTiles);
    }

    public bool Build(Turret turret)
    {
        if(TilesAvailable())
        {
            int randomIndex = Random.Range(0, turretTiles.Count);
            Vector2 buildPosition = turretTiles[randomIndex].transform.position;
            Turret newTurret = Instantiate(turret, buildPosition, Quaternion.identity, turretsParent);
            turretTiles.RemoveAt(randomIndex);
            return true;
        }

        return false;
    }

    private bool TilesAvailable()
    {
        return turretTiles.Count > 0;
    }
}
