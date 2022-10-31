using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float rotationSpeed = 1f;
    [SerializeField] protected float shootDelay = .5f;
    [SerializeField] protected float enemyDetectRadius = 2f;
    
    private Enemy _targetEnemy = null;
    
    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }

    private void FindTarget()
    {
        var position = transform.position;
        var colliders = Physics2D.OverlapCircleAll(new Vector2(position.x, position.y), 2f);

        var distance = float.MaxValue;
        foreach (var other in colliders)
        {
            if (!other.CompareTag("Enemy")) continue;

            var temporaryTarget = other.GetComponent<Enemy>();
            var remDistance = temporaryTarget.GetRemainingDistance();
            if (!(remDistance < distance)) continue;
            distance = remDistance;
            _targetEnemy = temporaryTarget;
        }
    }
}
