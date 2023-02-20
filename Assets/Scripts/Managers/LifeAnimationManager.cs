using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LifeAnimationManager : MonoBehaviour
{
    [SerializeField] private Image prefab;
    
    public static Queue<Image> allLives = new Queue<Image>();

    public static LifeAnimationManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static void StartAnimation(Vector3 startPosition)
    {
        if (allLives.Count == 0)
        {
            allLives.Enqueue(Instantiate(instance.prefab, instance.transform));
        }

        var life = allLives.Dequeue();

        life.transform.localScale = Vector3.one;
        
        Vector3 targetPos= DataHolder.HealthDisplay.LastChangedHeart.transform.position;

        DOTween.Sequence()
            .AppendCallback(() => life.transform.position = startPosition)
            .Append(life.transform.DOMove(targetPos, 1f))
            .AppendCallback(() => DataHolder.HealthManager.ChangeHealthValue(1))
            .Append(life.transform.DOScale(Vector3.one * 0.5f,1f))
            .AppendCallback(()=> life.transform.localScale = Vector3.zero)
            .AppendCallback(()=> allLives.Enqueue(life));
    }
}
