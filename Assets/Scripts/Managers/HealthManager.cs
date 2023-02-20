using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    public UnityAction<int> OnHealthChanged;
    public UnityAction OnHealthEnded;

    private int CurrentHealth { get; set; }
    public bool LivesIsFull => CurrentHealth >= maxHealth;

    [SerializeField] public int startHealth;
    [SerializeField][Range(1,20)] public int maxHealth;
    
    
    private void Awake()
    {
        DataHolder.HealthManager = this;
        CurrentHealth = startHealth;
    }

    public void ChangeHealthValue(int value)
    {
        CurrentHealth += value;
        
        
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            OnHealthEnded.Invoke();
        }
        OnHealthChanged.Invoke(CurrentHealth);
    }
}
