using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class UI_Timer : MonoBehaviour
{
    [SerializeField] private float _MAX_TIME = 300.0f;
    [SerializeField] private float _current_time;
    [SerializeField] private float width = 1600;
    [SerializeField] private float endWidth = 140;

    RectTransform rect;

    void Start()
    {
        _current_time = _MAX_TIME;

        rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width, rect.sizeDelta.y);
    }


    void Update()
    {
        if (_current_time <= 0)
        {
            return;
        }

        _current_time -= Time.deltaTime;
        rect.sizeDelta = new Vector2((width-endWidth) * (_current_time / _MAX_TIME)+endWidth, rect.sizeDelta.y);


    }
}
