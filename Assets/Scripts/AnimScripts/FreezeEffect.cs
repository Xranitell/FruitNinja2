using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FreezeEffect : MonoBehaviour
{
    [SerializeField] private float maxFadeValue;
    [SerializeField] private float animDuration;
    
    
    [SerializeField] private Image fadeImage;
    [SerializeField] private Image effectImage;
    [SerializeField] private Image blur;
    
    [SerializeField] float blueFadeMultiplier = 0.2f;
    [SerializeField] float borderTransparentMultiplier = 0.5f;
    
    
    public static FreezeEffect Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StartFreezeScreen()
    {
        DOTween.Sequence()
            .Append(fadeImage.DOFade(maxFadeValue * blueFadeMultiplier, animDuration))
            .Append(blur.DOFade(maxFadeValue, animDuration))
            .Append(effectImage.DOFade(maxFadeValue * borderTransparentMultiplier, animDuration))
            .SetUpdate(true);
    }

    public void EndFreezeScreen()
    {
        DOTween.Sequence()
            .Append(fadeImage.DOFade(0, animDuration))
            .Append(effectImage.DOFade(0, animDuration))
            .Append(blur.DOFade(0, animDuration))
            .SetUpdate(true);
    }

}

