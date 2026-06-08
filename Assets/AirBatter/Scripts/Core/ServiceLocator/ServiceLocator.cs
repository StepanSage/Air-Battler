using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator 
{
    public static ServiceLocator Instance => _instance ??= new();

    private static ServiceLocator _instance;
    private static readonly Dictionary<string, IService> _allService = new Dictionary<string, IService>();

    /// <summary>
    /// Rigister Service by type
    /// </summary>
    public void Rigister<T>(T service) where T : IService
    {
        string key = typeof(T).Name;

        if(_allService.ContainsKey(key))
        {
            Debug.LogError($"Serice is rigister {key}");
            return;
        }

        _allService.Add(key, service);
    }

    /// <summary>
    /// Delete service
    /// </summary>
    public void UnRigister<T>(T service) where T : IService
    {
        string key = typeof(T).Name;

        if (_allService.ContainsKey(key))
            _allService.Remove(key);
    }

    /// <summary>
    /// allows you to get a service by type
    /// </summary>
    public T Get<T>() where T : IService
    {
        string key = typeof(T).Name;

        if (!_allService.ContainsKey(key))
            Debug.LogError($"Serice not find {key}");

        return (T)_allService[key];
    }
}
