public class DamageblePlayer : Damageable
{
    public int countReserHealth;

    protected override void Start()
    {
        base.Start();
        _eventBus.Subscribe<RestorHealthEvent>(RestorHealth);
        _eventBus.Subscribe<AbilityAddHealthEvent>(AddHelthEvent);
        
    }

    public override void TakeDamage(int damage, WhoDamage whoDamage)
    {
        base.TakeDamage(damage, whoDamage);
        _eventBus.Publish<ChangeRenderingHealth>(new(-1));
        _eventBus.Publish<LostGameEvent>(new(_currentHealth));
    }

    protected void RestorHealth(RestorHealthEvent restorHealthEvent)
    {
        countReserHealth = MaxHealth - _currentHealth;
        _eventBus.Publish<ChangeRenderingHealth>(new(countReserHealth));
        ResetHealth();
    }

    protected void AddHelthEvent(AbilityAddHealthEvent abilityAddHealthEvent) 
    {

        AddHealth(abilityAddHealthEvent.AddHealth);
        _eventBus.Publish<ChangeRenderingHealth>(new(+1));
    }
}
