using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerData", order = 3)]
public class TowerData : ScriptableObject
{
    [SerializeField, Tooltip("Max Rotation(in degrees) per Second")]
    private float rotationSpeed;
    public float RotationSpeed => rotationSpeed;

    [SerializeField] private float shootDelay;
    public float ShootDelay => shootDelay;

    [SerializeField] private float enemyDetectRadius;
    public float EnemyDetectRadius => enemyDetectRadius;

    [SerializeField] private Bullet bulletPrefab;
    public Bullet BulletPrefab => bulletPrefab;

    [SerializeField] private int cost;
    public int Cost => cost;
}
