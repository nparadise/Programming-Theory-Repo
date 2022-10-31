using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Tower : MonoBehaviour
{
    [SerializeField] protected float rotationSpeed = 1f;
    [SerializeField] protected float shootDelay = .5f;

    private CircleCollider2D _circleCollider2D;
    
    private void Awake()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }
    
    
}
