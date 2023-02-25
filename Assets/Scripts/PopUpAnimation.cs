using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopUpAnimation : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject content;
    [SerializeField] private float animDuration;
    [SerializeField] private Image blur;

    [SerializeField] private float maxFadeValue;
    
    public UnityAction OnPopUpShowed;
    public UnityAction OnPopUpHided;
    
    
    public void ShowPopUp()
    {
        DOTween.Sequence()
            .AppendCallback(()=>OnPopUpShowed?.Invoke())
            .Append(fadeImage.DOFade(maxFadeValue, animDuration))
            .Append(blur.DOFade(maxFadeValue, animDuration))
            .Append(content.transform.DOScale(1, animDuration))
            .SetUpdate(true);
    }

    public void HidePopUp()
    {
        DOTween.Sequence()
            .AppendCallback(()=>OnPopUpHided?.Invoke())
            .Append(fadeImage.DOFade(0, animDuration))
            .Append(blur.DOFade(0, animDuration))
            .Append(content.transform.DOScale(0, animDuration))
            .SetUpdate(true);
    }
}
