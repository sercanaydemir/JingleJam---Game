using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class NotifText : MonoBehaviour
{
    private TextMeshProUGUI text;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        text.text = "";
    }

    private void OnEnable()
    {
        EventManager.OnSetText += SetText;
    }
    private void OnDisable()
    {
        EventManager.OnSetText -= SetText;
    }

    void SetText(string text)
    {
        this.text.text = text;
    }
}
