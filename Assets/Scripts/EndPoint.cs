using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private StageManager _stageManager;
    
    private void Start()
    {
        _stageManager = GameObject.Find("Stage Manager").GetComponent<StageManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            _stageManager.EndStage();
        }
    }
}
