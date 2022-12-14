using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class UI_Timer : MonoBehaviour
{
    public float _MAX_TIME;
    [SerializeField] private float _currentTime;
    public float width;
    public Vector2 currentSize;
    public float endWidth;

    RectTransform rect;
    
    public GameObject endCanvas;

    void Start()
    {
        _currentTime = _MAX_TIME;

        rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width, rect.sizeDelta.y);
    }


    void Update()
    {
        if (_currentTime <= 0 && Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
            endCanvas.SetActive(true);
        }
        else
        {
            _currentTime -= Time.deltaTime;
            currentSize = new Vector2((width-endWidth) * (_currentTime / _MAX_TIME)+endWidth, rect.sizeDelta.y);
            rect.sizeDelta = currentSize;    
        }
    }
}
