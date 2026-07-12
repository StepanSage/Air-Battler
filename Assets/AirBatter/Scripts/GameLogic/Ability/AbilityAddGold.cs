using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAddGold : Ability
{
    public override void Useing()
    {
        _eventBus?.Publish<AddGoldEvent>(new AddGoldEvent(AbilityConfig.CountAddGold));
    }
}
