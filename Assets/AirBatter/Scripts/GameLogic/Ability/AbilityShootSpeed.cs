public class AbilityShootSpeed : Ability
{
    public override void Useing()
    {
        _eventBus.Publish(new OnUpShootSpeedEvent(AbilityConfig.ShootSpeed));
    }
}

public struct OnUpShootSpeedEvent : IEvent
{
    public float Speed { get; private set;}

    public OnUpShootSpeedEvent(float speed)
    {
        Speed = speed;
    }

}
