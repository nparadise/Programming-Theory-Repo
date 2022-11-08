using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StagePathScriptableObject", order = 1)]
public class StagePathScriptableObject : ScriptableObject
{
    public Vector3[] pathPoints;
    
    private List<float> _distanceToEnd;

    private void OnEnable()
    {
        if (_distanceToEnd is not { Count: 0 }) return;
        if (pathPoints.Length == 0) return;

        // Initialize the list _distanceToEnd
        _distanceToEnd = new List<float>(pathPoints.Length - 1);
        for (var i = 0; i < pathPoints.Length - 1; i++)
        {
            _distanceToEnd.Add(0f);
        }
        
        for (var i = _distanceToEnd.Count - 1; i >= 0; i--)
        {
            _distanceToEnd[i] = Vector3.Distance(pathPoints[i], pathPoints[i + 1]);
        }
    }

    public float RemainingDistance(int nextPoint, Vector3 currentPosition)
    {
        if (nextPoint >= _distanceToEnd.Count) return 0;
        
        var rem = _distanceToEnd[nextPoint];
        rem += Vector3.Distance(currentPosition, pathPoints[nextPoint]);
        return rem;
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(StagePathScriptableObject))]
public class StagePathScriptableObjectEditor : Editor
{
    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        var obj = target as StagePathScriptableObject;
        if (!obj) return;

        for (var i = 0; i < obj.pathPoints.Length; i++)
        {
            var point = obj.pathPoints[i];
            Handles.Label(point, "" + i);
            if (i != obj.pathPoints.Length - 1)
            {
                Handles.color = Color.red;
                Handles.DrawLine(point, obj.pathPoints[i+1], 2f);
            }
            EditorGUI.BeginChangeCheck();
            var newPosition = Handles.PositionHandle(point, quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(obj, "Change path point position");
                obj.pathPoints[i] = newPosition;
            }
        }
    }
}

#endif

