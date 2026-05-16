using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ScenceLoader : MonoBehaviour, ISceneLoader
{
    private IEventBus _eventBus;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();

       
    }

    public Task Load(string scenceName, bool addivitive = false, CancellationToken ct = default)
    {
        throw new System.NotImplementedException();
    }

    public Task ReloadActive(CancellationToken ct = default)
    {
        throw new System.NotImplementedException();
    }

    public Task UnLoad(string scenceName, CancellationToken ct = default)
    {
        throw new System.NotImplementedException();
    }
}
