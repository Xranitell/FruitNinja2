using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Config : MonoBehaviour
{
    [BoxGroup("Physics")][SerializeField] public float gravity = 9.8f;
    [BoxGroup("Physics")][SerializeField] public float impulseMultiplier = 1;

    [BoxGroup("Components")] [SerializeField] public float shadowOffsetMultiplier = 0.07f;

    private void Awake()
    {
        DataHolder.Config = this;
    }
}
