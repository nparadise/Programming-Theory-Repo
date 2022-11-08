using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Camera _camera;
    private Canvas _canvas;
    private RectTransform _rectTransform;
    
    [SerializeField] private TowerPlacementUI _towerPlacementUI;
    private TowerPlacement _towerPlacement;

    [SerializeField] private GameObject gameOverUI;
    
    private TextMeshProUGUI _pointText;
    
    private void Start()
    {
        _camera = Camera.main;
        _canvas = GetComponent<Canvas>();
        _rectTransform = (RectTransform)transform;
        _pointText = _rectTransform.Find("Point").GetComponent<TextMeshProUGUI>();

        _towerPlacement = GameObject.Find("Player").GetComponent<TowerPlacement>();
    }

    /// <summary>
    /// Updates the point text with the given point.
    /// </summary>
    /// <param name="point"></param>
    public void UpdatePoint(int point)
    {
        if (!_pointText) return;
        _pointText.text = $"Point: {point}";
    }

    public void ShowTowerPlacementMenu()
    {
        var mousePositionInScreen = Input.mousePosition;
        var mousePositionInWorld = _camera.ScreenToWorldPoint(mousePositionInScreen);
        var mousePositionInCanvas = (Vector2)mousePositionInScreen / _canvas.scaleFactor;
        mousePositionInCanvas.y -= _rectTransform.rect.height;
        
        if (!_towerPlacement.CanPlaceTower(mousePositionInWorld))
        {
            _towerPlacementUI.Hide();
            return;
        }
        _towerPlacementUI.Show(mousePositionInCanvas, _rectTransform);
    }

    public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
    }
}
