using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StagePathScriptableObject")]
public class StagePathScriptableObject : ScriptableObject
{
    public Vector3[] pathPoints;

    private List<float> _distanceToEnd = null;
    
    private void Awake()
    {
        if (pathPoints == null || pathPoints.Length == 0) return;
        
        _distanceToEnd = new List<float>(pathPoints.Length - 1);
        for (var i = _distanceToEnd.Count - 1; i >= 0; i--)
        {
            _distanceToEnd[i] = Vector3.Distance(pathPoints[i], pathPoints[i + 1]);
        }
    }

    public float RemainingDistance(int nextPoint, Vector3 currentPosition)
    {
        if (_distanceToEnd == null) return -1f;
        
        var rem = _distanceToEnd[nextPoint];
        rem += Vector3.Distance(currentPosition, pathPoints[nextPoint]);
        
        return rem;
    }
}

