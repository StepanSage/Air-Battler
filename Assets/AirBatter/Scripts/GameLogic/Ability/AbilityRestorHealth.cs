public class RestorHealth : Ability
{
    public override void Useing()
    {
        _eventBus.Publish<RestorHealthEvent>(new RestorHealthEvent());
    }
}

public struct RestorHealthEvent: IEvent { }

