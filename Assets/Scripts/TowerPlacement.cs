using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum TowerType
{
    Basic,
    Cannon,
    Slow,
    Chain,
    Count
}

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    [SerializeField] private Tower[] _towerPrefabs;

    private Camera _camera;
    private Player _player;
    private UIManager _uiManager;

    private Vector3Int _positionToPlaceTower;
    
    private void Start()
    {
        _camera = Camera.main;
        _player = GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    /// <summary>
    /// Checks that the tower can be placed in given position.
    /// </summary>
    /// <param name="position2Check"> Position to check. </param>
    /// <returns> True if the tower can be placed. False if not. </returns>
    public bool CanPlaceTower(Vector3 position2Check)
    {
        var hitAll = Physics2D.GetRayIntersectionAll(new Ray(position2Check, Vector3.forward));
        var cellPos = _grid.WorldToCell(position2Check);
        
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
                if (hitTowerPos == cellPos)
                {
                    return false;
                }
            }
        }

        _positionToPlaceTower = cellPos;
        return true;
    }
    
    
    /// <summary>
    /// Place the tower with selected type of tower.
    /// </summary>
    /// <param name="type">Tower to be installed.</param>
    public void PlaceTower(TowerType type = TowerType.Basic)
    {
        var towerPos = _grid.GetCellCenterWorld(_positionToPlaceTower);
        // Check whether the tower is affordable. If affordable, use point and create tower, and if not, return. 
        if (!_player.UsePoint(_towerPrefabs[(int)type].Cost))
        {
            // TODO: Shows Not Enough Point Message UI.
            return;
        }
        Instantiate(_towerPrefabs[(int)type], towerPos, Quaternion.identity);
    }
}
