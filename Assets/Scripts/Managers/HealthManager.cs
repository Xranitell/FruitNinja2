using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    public UnityAction<int> OnHealthChanged;
    public UnityAction OnHealthEnded;

    public int CurrentHealth { get; private set; }

    [SerializeField] public int startHealth;
    [SerializeField] public int maxHealth;

    public bool LivesIsFull()
    {
        if (CurrentHealth >= maxHealth)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void Awake()
    {
        DataHolder.HealthManager = this;
        CurrentHealth = startHealth;
        
        if (CurrentHealth >= maxHealth)
        {
            LifeInfo.CanBeSpawned = false;
        }
    }

    private void Start()
    {
        ChangeHealthValue(0);
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
