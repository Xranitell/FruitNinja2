using System;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float maxTimeBetweenCuts = 0.1f;
    [SerializeField] private int maxComboCount = 10;

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
            
            AddPoints(_pointsInCombo * _multiplier);
            _multiplier = 0;
            _pointsInCombo = 0;
        }
    }

    public void RegisterNewCut(Fruit fruit)
    {
        if(maxComboCount > _multiplier)
        {
            _multiplier++;
        }

        _pointsInCombo += fruit.points;
        lastFruitPos = fruit.wholeBlock.transform.position;
        _timer = 0;
    }

    void AddPoints(int points)
    {
        _currentPoints += points;
        OnScoreUpdated.Invoke(_currentPoints);
    }
    
    
}
