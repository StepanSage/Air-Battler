public class DealBulletDamage : DealDamage
{
    private IEventBus _eventBus;

    private void Start()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _eventBus.Subscribe<AbilityDamageEvent>(SetDamageEvent);
    }

    public void SetDamageEvent(AbilityDamageEvent abilityDamageEvent)
    {
        SetDamage(Damage + abilityDamageEvent.Damage);
        
    }
}
