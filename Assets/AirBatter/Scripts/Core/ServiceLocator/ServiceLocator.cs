using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator 
{
    public static ServiceLocator Service;

    private static readonly Dictionary<string, IService> _allService = new Dictionary<string, IService>();

    public static void Initialization()
    {
        Service = new();
    }

    public T Get<T>() where T : IService
    {
        string key = typeof(T).Name;

        if (!_allService.ContainsKey(key))
        {
            Debug.LogError("Serice not find");
        }

        return (T)_allService[key];
    }

    public void Rigister<T>(T service) where T : IService
    {
        string key = typeof(T).Name;

        if(_allService.ContainsKey(key))
        {
            Debug.LogError("Serice is rigister");
            return;
        }

        _allService.Add(key, service);
    }

    public void UnRigister<T>(T service) where T : IService
    {
        string key = typeof(T).Name;

        if (_allService.ContainsKey(key))
        {
            _allService.Remove(key);
        }
    }
}
