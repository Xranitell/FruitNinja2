using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SamuraiTimer : MonoBehaviour
{
    static public SamuraiTimer instance;
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateValue(string newText)
    {
        text.text = newText;
    }
}
