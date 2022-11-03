using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance = null;
    
    [SerializeField] private StagePathScriptableObject _stagePath;
    [SerializeField] private Spawner spawner;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Temporary code for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartStage();
        }
    }

    public void StartStage()
    {
        spawner.StartSpawn();
    }

    public void EndStage()
    {
        
    }
}
