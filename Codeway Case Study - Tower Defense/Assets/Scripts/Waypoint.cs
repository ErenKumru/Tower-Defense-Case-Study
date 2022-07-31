using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private bool randomize;
    [SerializeField] private Waypoint[] nextWaypoints;
    private Waypoint nextWaypoint;

    public Waypoint GetNextWaypoint()
    {
        if (randomize) Randomize();
        else nextWaypoint = nextWaypoints[0];

        return nextWaypoint;
    }
    
    private void Randomize()
    {
        int randomIndex = Random.Range(0, nextWaypoints.Length);
        nextWaypoint = nextWaypoints[randomIndex];
    }
}
