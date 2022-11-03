using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyData", order = 2)]
public class EnemyData : ScriptableObject
{
    [SerializeField, Tooltip("per second")]
    private float speed;
    public float Speed
    {
        get => speed;
    }

    [SerializeField] private float maxHealth;
    public float MaxHealth
    {
        get => maxHealth;
    }

    [SerializeField] private int reward;
    public int Reward
    {
        get => reward;
    }
}
