using UnityEngine;

public class AbilityDamage : Ability
{
    public override void Useing()
    {
        _eventBus.Publish(new AbilityDamageEvent(AbilityConfig.Damage));
    }  
}

public struct AbilityDamageEvent : IEvent
{
    public int Damage;

    public AbilityDamageEvent(int damage)
    {
        this.Damage = damage;
    }
}