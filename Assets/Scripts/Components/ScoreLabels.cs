using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreLabels : MonoBehaviour
{
    [SerializeField] private TextAnimation currentScoreAnimation;
    [SerializeField] private TextAnimation recordScoreAnimation;
    public int CurrentScore { get; set; }
    public int RecordScore { get; set; }

    private void Awake()
    {
        RecordScore = PlayerPrefs.GetInt("Record");
        DataHolder.ScoreLabels = this;
    }

    private void Start()
    {
        DataHolder.ScoreManager.OnScoreUpdated += UpdateLabels;
        currentScoreAnimation.SetText("", 0);
        recordScoreAnimation.StartAnimation("Лучший: ",RecordScore);
    }

    private void UpdateLabels(int newValue)
    {
        currentScoreAnimation.StartAnimation("", newValue);
        CurrentScore = newValue;
            
        if (CurrentScore >= RecordScore)
        {
            recordScoreAnimation.StartAnimation("Лучший: ",newValue);
            RecordScore = newValue;
        }
    }
    
    
}
