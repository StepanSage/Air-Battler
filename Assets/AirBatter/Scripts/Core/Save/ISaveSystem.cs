public interface ISaveSystem : IService
{
    public void Save();
    public void Load();
    public Data GetData();
}
