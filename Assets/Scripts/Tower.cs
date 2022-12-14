using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Tower : MonoBehaviour
{
    [SerializeField] protected TowerData data;
    public int Cost => data.Cost;
    protected bool IsShootable = true;

    private HashSet<Enemy> _enemiesInRange;
    protected Enemy _targetEnemy;

    [SerializeField] protected Transform headCenter;
    [SerializeField] protected Transform muzzle;
    
    private void Awake()
    {
        _enemiesInRange = new HashSet<Enemy>();
        var cir = GetComponent<CircleCollider2D>();
        cir.radius = data.EnemyDetectRadius;
    }

    private void Update()
    {
        FindTarget();
        LookTarget();
        if (IsShootable && CheckCanShootTarget())
        {
            Shoot();

            if (data.ShootDelay > Single.Epsilon)
            {
                IsShootable = false;
                StartCoroutine(ShootDelay());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        _enemiesInRange.Add(other.GetComponent<Enemy>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        _enemiesInRange.Remove(other.GetComponent<Enemy>());
    }

    /// <summary>
    /// Find enemy which is closest to the end point and set it as the target.
    /// </summary>
    private void FindTarget()
    {
        if (_enemiesInRange.Count == 0)
        {
            _targetEnemy = null;
            return;
        }

        var distance = float.MaxValue;
        foreach (var other in _enemiesInRange)
        {
            var remainingDistance = other.GetRemainingDistance();
            if (!(remainingDistance < distance)) continue;
            distance = remainingDistance;
            _targetEnemy = other;
        }
    } 

    /// <summary>
    /// Rotate the tower head to look at enemy.
    /// </summary>
    private void LookTarget()
    {
        if (!_targetEnemy) return;
        var headTransform = headCenter.transform;
        var targetTransform = _targetEnemy.transform;
        
        var direction = targetTransform.position - headTransform.position;
        var remainingAngle = Quaternion.FromToRotation(headTransform.right, direction).eulerAngles.z;
        
        var isCCW = remainingAngle < 180f;
        remainingAngle = isCCW ? remainingAngle : 360f - remainingAngle;
        var angleToRotate = data.RotationSpeed * Time.deltaTime;
        angleToRotate = angleToRotate > remainingAngle ? remainingAngle : angleToRotate;
        headTransform.Rotate(0,0, isCCW ? angleToRotate : -angleToRotate);
    }

    private bool CheckCanShootTarget()
    {
        if (!_targetEnemy) return false;
        
        var angle = Quaternion.FromToRotation(headCenter.transform.right, _targetEnemy.transform.position - transform.position)
            .eulerAngles.z;
        angle = angle < 180f ? angle : 360 - angle;
        // Debug.Log($"Angle: {angle}");
        return angle < 2f;
    }
    
    protected virtual void Shoot()
    {
        // Debug.Log("Shoot");
        // Debug.DrawLine(transform.position, _targetEnemy.transform.position, Color.red, 1f);

        var spawnedBullet = Instantiate(data.BulletPrefab, muzzle.position, headCenter.rotation);
        spawnedBullet.SetTarget(_targetEnemy);
        spawnedBullet.HitTarget();        
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(data.ShootDelay);
        IsShootable = true;
    }
}
