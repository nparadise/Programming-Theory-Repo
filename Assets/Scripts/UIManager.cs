using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pointText;

    public void UpdatePoint(int point)
    {
        if (!_pointText) return;
        _pointText.text = $"Score: {point}";
    }
}
