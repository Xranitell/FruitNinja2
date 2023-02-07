using System;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float maxTimeBetweenCuts = 0.1f;
    public Vector3 lastFruitPos;
    public UnityAction<int> OnComboEnded;
    public UnityAction<int> OnScoreUpdated;
    
    private float _timer;
    private int _currentPoints;
    private int _multiplier;
    private int _pointsInCombo;

    private void Awake()
    {
        DataHolder.ScoreManager = this;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer >= maxTimeBetweenCuts && _multiplier != 0)
        {
            if (_multiplier > 1)
            {
                OnComboEnded.Invoke(_multiplier);
            }
            
            _currentPoints += _pointsInCombo;
            _multiplier = 0;
            _pointsInCombo = 0;
            AddPoints(_pointsInCombo * _multiplier);
        }
    }

    public void RegisterNewCut(Fruit fruit)
    {
        _multiplier++;
        _pointsInCombo += fruit.points;
        lastFruitPos = DataHolder.MainCamera.WorldToScreenPoint(fruit.wholeBlock.transform.position);
        _timer = 0;
    }

    void AddPoints(int points)
    {
        _currentPoints += points;
        OnScoreUpdated.Invoke(_currentPoints);
    }
    
    
}
