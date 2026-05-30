using System;
public interface IPoolObject<T> where T : class 
{
    ///<summary>
    ///Get one object from the pool
    ///</summary>
    public T Get();

    /// <summary>
    /// Return one oject to the pool 
    /// </summary>
    public void Return(T obj);

    /// <summary>
    /// Clean all object from the pool 
    /// </summary>
    public void Clean();

    /// <summary>
    /// Get the current pool size
    /// </summary>
    public int Size();

    public void Prefarm(int count);


}
