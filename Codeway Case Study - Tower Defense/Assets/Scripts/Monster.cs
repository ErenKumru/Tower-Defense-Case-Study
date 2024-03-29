using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    public Action<Monster> OnMonsterDeath;

    [SerializeField] private int health;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int coinsValue;
    private Waypoint currentWayPoint;
    private Vector2 targetWayPointPosition;

    public void Initialize(Waypoint waypoint)
    {
        currentWayPoint = waypoint;
        targetWayPointPosition = currentWayPoint.transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetWayPointPosition, moveSpeed * Time.deltaTime);

        if (WayPointReached())
        {
            transform.position = targetWayPointPosition; //Snap to waypoint's exact position
            currentWayPoint = currentWayPoint.GetNextWaypoint();
            targetWayPointPosition = currentWayPoint.transform.position;
        }
    }

    private bool WayPointReached()
    {
        if (Vector2.Distance(transform.position, targetWayPointPosition) < 0.01f) return true;
        return false;
    }

    public void GetDamaged(int damage)
    {
        if(health > 0) health -= damage;

        if(health <= 0)
        {
            health = 100;
            OnMonsterDeath?.Invoke(this);
        }
    }

    public int GetCoinsValue()
    {
        return coinsValue;
    }
}
