public class SsveSystemInstaller : Installer
{
    private ISaveSystem _saveSystem;
    public override void Install(ServiceLocator serviceLocator)
    {
        _saveSystem = new SaveSystem();
        _saveSystem.Load();
        serviceLocator.Rigister<ISaveSystem>(_saveSystem);
    }

    public override void Uninstall(ServiceLocator serviceLocator)
    {
        _saveSystem = null;
        serviceLocator.UnRigister<ISaveSystem>(_saveSystem);
    }
}
