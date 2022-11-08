using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{    
    [SerializeField] private StagePathScriptableObject _stagePath;
    [SerializeField] private Spawner spawner;
    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
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
        Time.timeScale = 0;
        spawner.StopSpawn();
        _uiManager.ShowGameOverUI();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
