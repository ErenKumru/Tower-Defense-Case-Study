using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public event Action<Bullet> OnTargetReached;

    [SerializeField] private BulletSo bulletSo;

    private Sprite bulletSprite;
    private int damage;
    private float speed;

    private Monster targetMonster;
    private Vector2 targetPosition;
    private bool isHit = false;


    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        bulletSprite = bulletSo.bulletSprite;
        GetComponent<SpriteRenderer>().sprite = bulletSprite;
        damage = bulletSo.damage;
        speed = bulletSo.speed;
    }

    public void FireAtTarget(Monster monster)
    {
        targetMonster = monster;
        targetPosition = targetMonster.transform.position;
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        while (!TargetReached())
        {
            TrackTarget();
            yield return null;
        }

        //Fizzle Shot: if no target to hit at target position = targeted monster is already dead -> reach target position and fizzle out
        if(!isHit) OnTargetReached?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Monster monster = otherCollider.GetComponent<Monster>();

        if(monster != null && monster == targetMonster)
        {
            isHit = true;
            targetMonster.GetDamaged(damage);
            OnTargetReached?.Invoke(this);
        }
    }

    private void TrackTarget()
    {
        targetPosition = targetMonster.transform.position;
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private bool TargetReached()
    {
        if (Vector2.Distance(transform.position, targetPosition) < 0.01f) return true;
        return false;
    }
}
