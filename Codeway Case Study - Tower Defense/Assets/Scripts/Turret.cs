using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretSO turretSO;
    [SerializeField] private Transform[] barrels;
    [SerializeField] private int cost;

    private Sprite turretSprite;
    private Bullet bulletPrefab;
    private float shootingRate;
    private float rotationSpeed;
    

    private Queue<Bullet> bulletPool = new Queue<Bullet>();

    private List<Monster> monsters = new List<Monster>();
    private Monster targetMonster;
    private bool isAlreadyShooting;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        turretSprite = turretSO.turretSprite;
        GetComponent<SpriteRenderer>().sprite = turretSprite;
        bulletPrefab = turretSO.bulletPrefab;
        shootingRate = turretSO.shootingRate;
        rotationSpeed = turretSO.shootingRate;
        cost = turretSO.cost;
    }

    private void StartShooting()
    {
        if(!isAlreadyShooting)
        {
            isAlreadyShooting = true;
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        while(MonstersInRange())
        {
            targetMonster = TargetMonsterInFront();

            if (targetMonster != null)
            {
                //lock to targetMonster and wait for locking to complete then start shooting

                foreach (Transform barrel in barrels)
                {
                    SpawnBullet(barrel);
                }
            }

            yield return new WaitForSeconds(shootingRate);
        }

        isAlreadyShooting = false;
    }
    
    private void SpawnBullet(Transform barrel)
    {
        if (BulletAvailable())
        {
            Bullet bulletToShoot = bulletPool.Dequeue();
            bulletToShoot.gameObject.SetActive(true);
            bulletToShoot.OnTargetReached += CollectBullet;
            bulletToShoot.transform.position = barrel.position;
            bulletToShoot.FireAtTarget(targetMonster);
        }
        else
        {
            Bullet newBullet = Instantiate(bulletPrefab, barrel);
            newBullet.OnTargetReached += CollectBullet;
            newBullet.transform.position = barrel.position;
            newBullet.FireAtTarget(targetMonster);
        }
    }

    private void CollectBullet(Bullet bulletToCollect)
    {
        if (bulletToCollect != null)
        {
            bulletPool.Enqueue(bulletToCollect);
            bulletToCollect.OnTargetReached -= CollectBullet;
            bulletToCollect.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Monster monsterEntered = otherCollider.GetComponent<Monster>();
        monsters.Add(monsterEntered);
        //listen monster
        StartShooting();
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        Monster monsterExited = otherCollider.GetComponent<Monster>();
        monsters.Remove(monsterExited);
        //stop listening to monster
    }

    private Monster TargetMonsterInFront()
    {
        if (MonstersInRange()) return monsters[0];
        return null;
    }

    private bool MonstersInRange()
    {
        return monsters.Count > 0;
    }

    private bool BulletAvailable()
    {
        return bulletPool.Count > 0;
    }

    public int GetCost()
    {
        return cost;
    }
}
