using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool isTargetAssigned = false;
    
    private Enemy _targetEnemy;
    private Vector3 _targetPosition;

    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float damage = 10f;

    public void SetTarget(Enemy target)
    {
        if (isTargetAssigned) return;
        isTargetAssigned = true;
        _targetEnemy = target;
    }

    public void SetTarget(Vector3 target)
    {
        if (isTargetAssigned) return;
        isTargetAssigned = true;
        _targetPosition = target;
    }
    
    public void HitTarget()
    {
        if (_targetEnemy)
        {
            StartCoroutine(nameof(FollowEnemy));
        }
        else
        {
            StartCoroutine(nameof(HitPosition));
        }        
    }
    
    private IEnumerator FollowEnemy()
    {
        while (_targetEnemy)
        {
            transform.Translate(speed * Time.deltaTime * (_targetEnemy.transform.position - transform.position).normalized);
            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator HitPosition()
    {
        while (gameObject)
        {
            transform.Translate(speed * Time.deltaTime * (_targetPosition - transform.position).normalized);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_targetEnemy) return;
        if (other.gameObject == _targetEnemy.gameObject)
        {
            // Debug.Log("Hit Target Enemy");
            _targetEnemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
