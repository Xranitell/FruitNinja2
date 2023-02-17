using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(TMP_Text))]public class TextAnimation : MonoBehaviour
{
    [SerializeField] private float timeToUpdate;
    [SerializeField] private TMP_Text tmpText;


    private string _additiveText;
    private int _currentValue;

    int from;
    int to;


    static float t = 0.0f;
    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if(_currentValue < to)
        {
            _currentValue = Mathf.CeilToInt(Mathf.Lerp(from, to, t));

            t += Time.deltaTime;

            SetText(_additiveText, _currentValue);
        }
    }

    public void StartAnimation(string additiveWords, int newValue)
    {
        _additiveText = additiveWords;

        from = _currentValue;
        to = newValue;
        t = 0;
    }
    
    
    public void SetText(string textAdditive, int value)
    {
        tmpText.text = textAdditive + value;
    }
}

