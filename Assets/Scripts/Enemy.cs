using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected int reward = 1;
    [SerializeField] protected float health = 100f;
    
    [SerializeField] private List<Vector3> targets;
    private int _currentTargetPositionIndex = 0;
    
    private void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        var currentPosition = transform.position;
        if (Vector3.Distance(currentPosition, targets[_currentTargetPositionIndex]) < Vector3.kEpsilon && _currentTargetPositionIndex != targets.Count)
        {
            _currentTargetPositionIndex += 1;
        }
        
        var targetPosition = targets[_currentTargetPositionIndex];
        var direction = (targetPosition - currentPosition).normalized;
        transform.Rotate(Quaternion.FromToRotation(transform.right, direction).eulerAngles);
        transform.Translate(Time.deltaTime * speed * direction, Space.World);
    }
}
