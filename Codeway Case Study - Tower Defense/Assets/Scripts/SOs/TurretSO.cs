using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Create Turret/Turret", order = 1)]
public class TurretSO : ScriptableObject
{
    public Sprite turretSprite;
    public Bullet bulletPrefab;
    public float shootingRate;
    public float rotationSpeed;
}
