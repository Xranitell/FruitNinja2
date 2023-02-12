using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using System.Linq;

public class HealthDisplay : MonoBehaviour
{
    public GameObject heartPrefab;
    
    private readonly List<GameObject> _pullOfHearts = new List<GameObject>();
    private int _countOfActiveHearts;

    public GameObject LastChangedHeart => _pullOfHearts[_countOfActiveHearts-1];

    private void Awake()
    {
        DataHolder.HealthDisplay = this;
        DataHolder.HealthManager.OnHealthChanged += DisplayNewHealthValue;
        
        for (int i = 0; i < DataHolder.HealthManager.maxHealth; i++)
        {
            var heart = Instantiate(heartPrefab, transform);
            _pullOfHearts.Add(heart);

            heart.SetActive(i < DataHolder.HealthManager.startHealth);

            _countOfActiveHearts = DataHolder.HealthManager.startHealth;
        }
    }

    private void DisplayNewHealthValue(int newValue)
    {
        if (newValue > _countOfActiveHearts)
        {
            for (int i = _countOfActiveHearts; i < newValue; i++)
            {
                AnimateHeart(false,_pullOfHearts[i]);
            }
        }
        else
        {
            for (int i = _countOfActiveHearts; i > newValue; i--)
            {
                AnimateHeart(true,_pullOfHearts[i-1]);
            }
        }
        _countOfActiveHearts = newValue;
    }

    void AnimateHeart(bool isDestroyAnimation ,GameObject heart)
    {
        if (heart.active == false)
        {
            heart.SetActive(true);
        }
        
        var endValue = isDestroyAnimation ? 0 : 1;
        heart.transform.localScale = isDestroyAnimation ? Vector3.one : Vector3.zero;

        DOTween.Sequence()
            .Append(heart.transform.DOScale(endValue, 1))
            .AppendCallback(()=>heart.gameObject.SetActive(!isDestroyAnimation));
    }
}
