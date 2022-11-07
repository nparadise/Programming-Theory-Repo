using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected bool IsTargetSet = false;
    protected Enemy TargetEnemy;
    
    [SerializeField] protected float speed = 1.5f;
    [SerializeField] protected float damage = 10f;

    public void SetTarget(Enemy target)
    {
        if (IsTargetSet) return;
        IsTargetSet = true;
        TargetEnemy = target;
    }
    
    public virtual void HitTarget()
    {
        StartCoroutine(FollowEnemy());
    }
    
    private IEnumerator FollowEnemy()
    {
        while (TargetEnemy)
        {
            var dir = (TargetEnemy.transform.position - transform.position).normalized;
            transform.Rotate(Quaternion.FromToRotation(transform.right, dir).eulerAngles);
            transform.Translate(speed * Time.deltaTime * dir, Space.World);
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsTargetSet) return;
        if (other.gameObject == TargetEnemy.gameObject)
        {
            // Debug.Log("Hit Target Enemy");
            OnHitTarget(TargetEnemy);
            Destroy(gameObject);
        }
    }

    protected virtual void OnHitTarget(Enemy firstHit)
    {
        firstHit.TakeDamage(damage);
    }
}
