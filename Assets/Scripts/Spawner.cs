using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefabs;
    
    private int _enemyToSpawn;
    private float _spawnNumber;
    private float _spawnDelay;

    private Player _player;
    
    private WaitForSeconds _wait;
    
    private void Start()
    {
        _enemyToSpawn = 0;
        _spawnNumber = 10;
        _spawnDelay = 0.5f;
        _wait = new WaitForSeconds(_spawnDelay);

        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private IEnumerator Spawn()
    {
        for (var i = 0; i < _spawnNumber; i++)
        {
            var tf = transform;
            var enemy = Instantiate(enemyPrefabs[_enemyToSpawn], tf.position, tf.rotation);
            enemy.onAddPoint = _player.onAddPoint;
            yield return _wait;
        }
    }

    public void StartSpawn()
    {
        StartCoroutine(nameof(Spawn));
    }
}
