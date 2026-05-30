using System;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 10;

    private int _currentHealth = 0;
    private IEventBus _eventBus;

    public int MaxHealth => _maxHealth;

    public bool IsAlive => _currentHealth > 0;

    public event Action<int, int> OnHealthChange;
    public event Action OnChangHealth;
    public event Action OnDeath;


    private void Start()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!IsAlive)
            return;

        _currentHealth = Mathf.Max(0, _currentHealth - damage);
        OnHealthChange?.Invoke(_currentHealth, _maxHealth);
        OnChangHealth?.Invoke();

        Die();

    }

    private void Die()
    {
        if (_currentHealth <= 0)
        {
            _eventBus.Publish(new ExpEvent());
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
           
            
    }
}
