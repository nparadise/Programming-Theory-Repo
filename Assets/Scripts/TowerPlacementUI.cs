using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlacementUI : MonoBehaviour
{
    private TowerPlacement _towerPlacement; 
    
    [SerializeField] private Button[] _buttons;

    private void Start()
    {
        _towerPlacement = GameObject.Find("Player").GetComponent<TowerPlacement>();
        
        for (var i = 0; i < (int)TowerType.Count; i++)
        {
            var i1 = i;
            _buttons[i].onClick.AddListener(() => { _towerPlacement.PlaceTower((TowerType)i1); Hide(); });
        }
    }

    /// <summary>
    /// Shows the Tower Placement Menu at given position in parent canvas. 
    /// </summary>
    /// <param name="position"> Position in canvas </param>
    /// <param name="parentRectTr"> RectTransform of the parent's canvas. </param>
    public void Show(Vector2 position, RectTransform parentRectTr)
    {
        var rectTr = (RectTransform)transform;
        var rect = rectTr.rect;

        if (position.y - rect.height < -parentRectTr.rect.height)
        {
            position.y += rect.height;
        }

        if (position.x + rect.width > parentRectTr.rect.width)
        {
            position.x -= rect.width;
        }

        rectTr.anchoredPosition = position;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the Tower Placement Menu.
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
