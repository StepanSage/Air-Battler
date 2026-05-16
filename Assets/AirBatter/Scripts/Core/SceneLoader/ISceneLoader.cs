using System.Threading;
using System.Threading.Tasks;

public interface ISceneLoader 
{
    public Task Load(string scenceName, bool addivitive = false, CancellationToken ct = default);
    public Task UnLoad(string scenceName, CancellationToken ct = default);
    public Task ReloadActive(CancellationToken ct = default);
}
