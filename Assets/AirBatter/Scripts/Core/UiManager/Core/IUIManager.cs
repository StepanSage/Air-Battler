public interface IUIManager : IService
{
    public void ShowScreen<T>() where T : BaseScreen;

    public void HideScreen<T>() where T : BaseScreen;

    public BaseScreen GetScreen<T>() where T : BaseScreen;

    public void Dispose();
}
