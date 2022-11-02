using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected int reward = 1;
    [SerializeField] protected float currentHealth = 100f;
    
    [SerializeField] private StagePathScriptableObject path;
    private int _currentTargetPositionIndex = 0;

    private const float ArriveEpsilon = 0.01f;

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
        
        transform.Translate(Time.deltaTime * speed * direction, Space.World);
    }
    
    public float GetRemainingDistance()
    {
        return path.RemainingDistance(_currentTargetPositionIndex, transform.position);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        // Debug.Log($"Health: {currentHealth}");
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}


