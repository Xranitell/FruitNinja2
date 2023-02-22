using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimation : MonoBehaviour
{
    public static EventAnimation instance;
    [SerializeField] private Animator anim;

    private void Awake()
    {
        instance = this;
    }

    public void StartAnimation()
    {
        anim.SetTrigger("StartSamuraiEvent");
    }
    
}
