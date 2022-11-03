using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Tower : MonoBehaviour
{
    [SerializeField] protected TowerData data;
    private bool _isShootable = true;

    private HashSet<Enemy> _enemiesInRange;
    private Enemy _targetEnemy;

    [SerializeField] private Transform headCenter;
    [SerializeField] private Transform muzzle;
    
    private void Awake()
    {
        _enemiesInRange = new HashSet<Enemy>();
        GetComponent<CircleCollider2D>().radius = data.EnemyDetectRadius;
    }

    private void Update()
    {
        FindTarget();
        LookTarget();
        if (_isShootable && CheckCanShootTarget())
        {
            Shoot();
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
    
    private void Shoot()
    {
        // Debug.Log("Shoot");
        Debug.DrawLine(transform.position, _targetEnemy.transform.position, Color.red, 1f);

        var spawnedBullet = Instantiate(data.BulletPrefab, muzzle.position, Quaternion.identity);
        spawnedBullet.TargetEnemy = _targetEnemy;
        
        _isShootable = false;
        StartCoroutine(ShootDelay());
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(data.ShootDelay);
        _isShootable = true;
    }
}
