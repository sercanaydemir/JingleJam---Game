using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image filledImage;


    private void OnEnable()
    {
        EventManager.OnHealthChanged += SetValue;
    }
    
    private void OnDisable()
    {
        EventManager.OnHealthChanged -= SetValue;
    }

    void SetValue(float v)
    {
        filledImage.fillAmount = v/100;
    }
}
