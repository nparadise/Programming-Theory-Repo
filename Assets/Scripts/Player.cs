using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntEvent : UnityEvent<int>
{
}

public class Player : MonoBehaviour
{
    private UIManager _ui;

    public IntEvent onAddPoint;

    private int _point = 0;
    
    private void Start()
    {
        _ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (!_ui) { Debug.Log("UI Manager is Null"); }

        onAddPoint ??= new IntEvent();
        onAddPoint.AddListener(AddPoint);
    }

    private void OnDestroy()
    {
        onAddPoint.RemoveAllListeners();
    }

    private void AddPoint(int add)
    {
        _point += add;
        _ui.UpdatePoint(_point);
    }

    public bool UsePoint(int use)
    {
        if (use > _point) return false;
        _point -= use;
        _ui.UpdatePoint(_point);
        return true;
    }
}
