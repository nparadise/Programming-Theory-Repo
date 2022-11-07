using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    [SerializeField] private Tower _tower;

    private Camera _camera;
    
    private void Start()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        // When click right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            var cellPos = _grid.WorldToCell(mousePos);
            if (CanPlaceTower(cellPos)) PlaceTower(cellPos);
        }
    }

    private bool CanPlaceTower(Vector3Int cellPos)
    {
        var hitAll = Physics2D.GetRayIntersectionAll(_camera.ScreenPointToRay(Input.mousePosition));
        if (hitAll.Length == 0) return false;

        foreach(var hit in hitAll)
        {
            var hitTr = hit.transform;
            if (hitTr.CompareTag("Path"))
            {
                return false;
            }
            if (hitTr.CompareTag("Tower"))
            {
                var hitTowerPos = _grid.WorldToCell(hitTr.position);
                Debug.Log($"Hit: {hitTr.name}, HitTowerPosition: {hitTowerPos}\nPosition2Place: {cellPos}");
                if (hitTowerPos == cellPos)
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    private void PlaceTower(Vector3Int cellPos)
    {
        var towerPos = _grid.GetCellCenterWorld(cellPos);
        Instantiate(_tower, towerPos, Quaternion.identity);
    }
}
