using UnityEngine;

public class BulletFactory : UniversalFactory
{
    private IEventBus _eventBus;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
    }

    public override GameObject Spawn()
    {
        _eventBus.Publish(new OnShotEvent());
        return base.Spawn();
    }
}
