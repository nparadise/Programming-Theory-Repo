using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class CannonBullet : Bullet
{
    [SerializeField] private float _explosionRadius = 1f;

    // POLYMORPHISM
    protected override void OnHitTarget(Enemy firstHit)
    {
        base.OnHitTarget(firstHit);
        var pos = new Vector2(transform.position.x, transform.position.y);
        var overlapped = Physics2D.OverlapCircleAll(pos, _explosionRadius);
        foreach (var other in overlapped)
        {
            if (!other.CompareTag("Enemy")) continue;
            var dist = Vector3.Distance(firstHit.transform.position, other.transform.position);
            var reducedDamage = damage * dist / _explosionRadius;
            other.GetComponent<Enemy>().TakeDamage(reducedDamage);
        }
    }
}
