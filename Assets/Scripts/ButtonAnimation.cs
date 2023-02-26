using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] UnityEvent onAnimateEnded;
    [SerializeField] UnityEvent onAnimateStarted;
    
    [SerializeField] private float animationDuration;
    
    private Image _image;
    private TMP_Text _tmpText;
    private void Start()
    {
        _image = GetComponent<Image>();
        _tmpText = GetComponentInChildren<TMP_Text>();
    }

    public void Animate()
    {
        onAnimateStarted.Invoke();

        DOTween.Sequence()
            .Append(transform.DOScale(0.8f, animationDuration))
            .Append(_image.DOColor(Color.gray, animationDuration))
            .Append(_tmpText.DOColor(Color.gray, animationDuration))
            
            .AppendCallback(()=> onAnimateEnded.Invoke())
            .Append(transform.DOScale(1f, animationDuration))
            .Append(_image.DOColor(Color.white, animationDuration))
            .Append(_tmpText.DOColor(Color.white, animationDuration))
            
            .SetUpdate(true);
    }
}
