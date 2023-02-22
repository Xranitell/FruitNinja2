using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = UnityEngine.SceneManagement.Scene;

[RequireComponent(typeof(Image))]
public class Fade : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private bool needStartAnimation;
    private void Start()
    {
        var image = GetComponent<Image>();
        if (needStartAnimation)
        {
            DOTween.Sequence().
                AppendCallback(()=>image.color = Color.black).
                Append(image.DOFade(0, fadeDuration)).
                SetEase(Ease.InQuad).
                AppendInterval(0.5f).SetUpdate(true);
        }
    }

    public void LoadNewScene(string sceneName)
    {
        var image = GetComponent<Image>();
        
        DOTween.Sequence().
            AppendCallback(()=>image.color = Color.clear).
            Append(image.DOFade(1, fadeDuration)).
            SetEase(Ease.InQuad).
            AppendInterval(0.5f).
            AppendCallback(()=>SceneManager.LoadScene(sceneName));
    }
}
