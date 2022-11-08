using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class IntEvent : UnityEvent<int> {}

public class Player : MonoBehaviour
{
    private UIManager _uiManager;

    public IntEvent onAddPoint;

    private int _point = 20;
    
    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (!_uiManager) { Debug.Log("UI Manager is Null"); }
        _uiManager.UpdatePoint(_point);
        
        onAddPoint ??= new IntEvent();
        onAddPoint.AddListener(AddPoint);
    }

    private void OnDestroy()
    {
        onAddPoint.RemoveAllListeners();
    }

    private void AddPoint(int add)
    {
        if (add < 0)
        {
            Debug.LogError("Negative value is not allowed. Use \"UsePoint(int)\" method.");
            return;
        }
        _point += add;
        _uiManager.UpdatePoint(_point);
    }

    public bool UsePoint(int use)
    {
        if (use < 0)
        {
            Debug.LogError("The parameter must be positive value");
            return false;
        }
        
        if (use > _point)
        {
            return false;
        }
        _point -= use;
        _uiManager.UpdatePoint(_point);
        return true;
    }
}
