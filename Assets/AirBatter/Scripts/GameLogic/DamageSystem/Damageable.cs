using System;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    public int MaxHealth; /*{ get; protected set; }*/
    public bool IsAlive => _currentHealth > 0;

    public int _currentHealth = 0;

    public event Action<int, int> OnHealthChangeRange;
    public event Action OnChangHealth;
    public event Action OnDeath;

    protected IEventBus _eventBus;

    protected virtual void Start()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        BaseStartSetting();
    }

    private void BaseStartSetting()
    {
        MaxHealth = 1;
        _currentHealth = MaxHealth;
    }

    public virtual void TakeDamage(int damage, WhoDamage whoDamage)
    {
        if (!IsAlive)
            return;

        _currentHealth = Mathf.Max(0, _currentHealth - damage);
        OnHealthChangeRange?.Invoke(_currentHealth, MaxHealth);
        OnChangHealth?.Invoke();

        Die(whoDamage);


    }

    protected virtual void Die(WhoDamage whoDamage)
    {

        if (_currentHealth <= 0 && whoDamage == WhoDamage.Player)
        {
            _eventBus.Publish(new AddExperineceEvent());
            _eventBus.Publish(new AddScoreKillEvent());
            LogicDie();
        }
        else if (_currentHealth <= 0)
        {
            LogicDie();
        }      
    }

    private void LogicDie()
    {
        ResetHealth();
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }

    protected void ResetHealth()
    {
        _currentHealth = MaxHealth;
        OnHealthChangeRange?.Invoke(_currentHealth, MaxHealth);
    }

    protected void AddHealth(int heath)
    {
        _currentHealth += heath;
        MaxHealth += heath;
        OnHealthChangeRange?.Invoke(_currentHealth, MaxHealth);
    }
}



