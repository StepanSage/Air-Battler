public class EventBusInstaller : Installer
{
    private IEventBus _instance; 

    public override void Install(ServiceLocator serviceLocator)
    {
        _instance = new EventBus();
        serviceLocator.Rigister<IEventBus>(_instance);
    }

    public override void Uninstall(ServiceLocator serviceLocator)
    {
        serviceLocator.UnRigister<IEventBus>(_instance);
        _instance = null;
    }
}
    

