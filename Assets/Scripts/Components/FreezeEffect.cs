using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FreezeEffect : MonoBehaviour
{
    private static FreezeEffect freeze;
    public Animator anim;

    private void Awake()
    {
        freeze = this;
    }

    public static void StartFreezeScreen() => freeze.anim.SetTrigger("startFreeze");
    public static void EndFreezeScreen() => freeze.anim.SetTrigger("endFreeze");
}

