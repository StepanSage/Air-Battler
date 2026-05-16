public interface IInstaller 
{
    public void Install(ServiceLocator serviceLocator);
    public void Uninstall(ServiceLocator serviceLocator);
    
}
