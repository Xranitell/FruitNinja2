using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    private int _currentValue;
    [SerializeField] private float timeToUpdate;

    private TMP_Text _tmpText;
    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
    }

    public void StartAnimation( int newValue, string additiveWords)
    {
        StartCoroutine(AnimCoroutine(newValue, additiveWords));
    }

    private IEnumerator AnimCoroutine(int to, string additiveWords)
    {
        var speed = timeToUpdate / (to - (int)_currentValue);
        
        while (_currentValue <= to)
        {
            _currentValue++;
            _tmpText.text = additiveWords + _currentValue;
            yield return new WaitForSeconds(speed);
        }
    }
    public void SetText(string textAdditive, int value) => _tmpText.text = textAdditive + value;
}

