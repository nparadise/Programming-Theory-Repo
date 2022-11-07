using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData data;
    protected float CurrentHealth;

    public bool _isDead;
    // public bool IsDead => _isDead;
    
    [SerializeField] private StagePathScriptableObject path;
    private int _currentTargetPositionIndex = 0;

    private const float ArriveEpsilon = 0.01f;

    public IntEvent onAddPoint;

    private void Awake()
    {
        if (!data)
        {
            Debug.LogError("Cannot find data scriptable object");
        }

        CurrentHealth = data.MaxHealth;
        _isDead = false;
    }

    private void Update()
    {
        Move();
    }

    private void OnDestroy()
    {
        // Debug.Log($"This Game Object({gameObject.name}) is destroyed. IsDead: {_isDead}");
    }

    protected virtual void Move()
    {
        var currentPosition = transform.position;
        // TODO: move path of the stage to the separate GameManager script 
        if (Vector3.Distance(currentPosition, path.pathPoints[_currentTargetPositionIndex]) < ArriveEpsilon && _currentTargetPositionIndex != path.pathPoints.Length)
        {
            _currentTargetPositionIndex += 1;
        }
        
        var targetPosition = path.pathPoints[_currentTargetPositionIndex];
        var direction = (targetPosition - currentPosition).normalized;
        transform.Rotate(Quaternion.FromToRotation(transform.right, direction).eulerAngles);
        
        transform.Translate(Time.deltaTime * data.Speed * direction, Space.World);
    }
    
    public float GetRemainingDistance()
    {
        return path.RemainingDistance(_currentTargetPositionIndex, transform.position);
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        // Debug.Log($"Health: {currentHealth}");
        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        onAddPoint.Invoke(data.Reward);
        Destroy(gameObject);
    }
}


