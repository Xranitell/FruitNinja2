using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public GameObject heartPrefab;
    private List<GameObject> _hearts = new List<GameObject>();
    private void Awake()
    {
        DataHolder.HealthManager.OnHealthChanged += DisplayNewHealthValue;
    }

    private void Start()
    {
        for (int i = 0; i < DataHolder.HealthManager.maxHealth; i++)
        {
            _hearts.Add(Instantiate(heartPrefab, transform));

            _hearts[i].SetActive(i < DataHolder.HealthManager.startHealth);
        }
    }

    private void DisplayNewHealthValue(int newValue)
    {
        for (int i = 0; i < _hearts.Count-1; i++)
        {
            bool isActive = i < newValue;
            _hearts[i].SetActive(isActive);
        }

    }
}
