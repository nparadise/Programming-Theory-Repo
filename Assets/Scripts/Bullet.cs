using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool isTargetAssigned = false;
    
    private Enemy _targetEnemy;
    public Enemy TargetEnemy
    {
        set
        {
            if (isTargetAssigned) return;
            _targetEnemy = value;
            isTargetAssigned = true;
        } 
    }

    private Vector3 _targetPosition;
    public Vector3 TargetPosition
    {
        set
        {
            if (isTargetAssigned) return;
            _targetPosition = value;
            isTargetAssigned = true;
        }
    }

    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float damage = 10f;

    private void Update()
    {
        if (!isTargetAssigned) return;
        
        if (_targetEnemy)
        {
            HitTarget();
        }
        else
        {
            HitPosition();
        }
    }
    
    private void HitTarget()
    {
        transform.Translate(speed * Time.deltaTime * (_targetEnemy.transform.position - transform.position).normalized);
    }

    private void HitPosition()
    {
        transform.Translate(speed * Time.deltaTime * (_targetPosition - transform.position).normalized);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_targetEnemy) return;
        if (other.gameObject == _targetEnemy.gameObject)
        {
            Debug.Log("Hit Target Enemy");
            Destroy(gameObject);
        }
    }
}
