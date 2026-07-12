using UnityEngine;

public class AbilityAddHealth : Ability
{
    public override void Useing()
    {
        _eventBus?.Publish<AbilityAddHealthEvent>(new AbilityAddHealthEvent(1));
    }
}

public struct AbilityAddHealthEvent: IEvent
{
    public int AddHealth;
    public AbilityAddHealthEvent(int addHealth)
    {
        AddHealth = addHealth;
    }
}
