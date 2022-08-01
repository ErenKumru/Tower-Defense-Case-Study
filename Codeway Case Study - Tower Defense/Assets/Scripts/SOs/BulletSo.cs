using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Create Turret/Bullet", order = 2)]
public class BulletSo : ScriptableObject
{
    public Sprite bulletSprite;
    public int damage;
    public float speed;
}
