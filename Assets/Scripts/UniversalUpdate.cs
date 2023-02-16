using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UniversalUpdate : MonoBehaviour
{
    public static UniversalUpdate Instance;
    [Range(0, 20)] public float tickRate;

    public UnityAction OnTick;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(UniversalUpdate_(tickRate));
    }

    IEnumerator UniversalUpdate_(float tickRate)
    {
        while (true)
        {
            yield return new WaitForSeconds(tickRate);
            OnTick.Invoke();
        }
    }
}
