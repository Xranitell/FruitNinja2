using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = UnityEngine.SceneManagement.Scene;

public class Fade : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    private void Start()
    {
        var image = GetComponent<Image>();
        DOTween.Sequence().
            AppendCallback(()=>image.color = Color.black).
            Append(image.DOFade(0, fadeDuration)).
            SetEase(Ease.InQuad).
            AppendInterval(0.5f);
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
