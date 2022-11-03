using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float CurrentHealth;
    [SerializeField] protected EnemyData data;
    
    [SerializeField] private StagePathScriptableObject path;
    private int _currentTargetPositionIndex = 0;

    private const float ArriveEpsilon = 0.01f;

    private void Awake()
    {
        if (!data)
        {
            Debug.LogError("Cannot find data scriptable object");
        }

        CurrentHealth = data.MaxHealth;
    }

    private void Update()
    {
        Move();
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
        Destroy(gameObject);
    }
}


