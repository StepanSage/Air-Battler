using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceLoader : MonoBehaviour, ISceneLoader
{
    private IEventBus _eventBus;
    private CancellationTokenSource _cts;
    private string _currentLoadingScence;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void Dispose()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();

        _eventBus.Subscribe<LoadScenceRequst>(OnLoadRequsted);
        _eventBus.Subscribe<UnloadScenceRequest>(OnUnloadRequst);
        _eventBus.Subscribe<ReloadActiveRequst>(OnReloadRequsted);

        _cts.Cancel();
        _cts.Dispose();
        _cts = null;
    }

    private void SubscribeEvents()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();

        _eventBus.Subscribe<LoadScenceRequst>(OnLoadRequsted);
        _eventBus.Subscribe<UnloadScenceRequest>(OnUnloadRequst);
        _eventBus.Subscribe<ReloadActiveRequst>(OnReloadRequsted);
    }

    private async void OnReloadRequsted(ReloadActiveRequst raq)
    {
        string active = SceneManager.GetActiveScene().name;
        await Load(active, additive: false, activateOnLoad: true);
    }

    private async void OnUnloadRequst(UnloadScenceRequest req)
    {
        try
        {
            await UnLoad(req.Scence);
            _eventBus.Publish(new SceneUnloadCompleted(req.Scence));
        }
        catch(System.Exception ex)
        {
            Debug.LogError($"[Sceneloader] Error unloading '{req.Scence}' : {ex} ");
        }
    }

    private async void OnLoadRequsted(LoadScenceRequst req)
    {
        try
        {
            if (req.CanclePrevious && _cts != null)
            {
                _cts.Cancel();
                _eventBus.Publish(new ScenceLoadCanceled(_currentLoadingScence));
            }

            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            _currentLoadingScence = req.Scence;

            await Load(req.Scence, req.Addivite, req.ActivateOnLoad, _cts.Token);

        }
        catch (System.OperationCanceledException)
        {
            _eventBus.Publish(new ScenceLoadCanceled(req.Scence));
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"{req.Scence}");
        }
    }

    public async Task Load(string scenceName, bool addivitive = false, CancellationToken ct = default)
        => await Load(scenceName, addivitive, activateOnLoad : true, ct);
    

    public async Task ReloadActive(CancellationToken ct = default)
    {
        var active = SceneManager.GetActiveScene().name;
        await Load(active, additive: false, activateOnLoad: true, ct);
    }

    public async Task UnLoad(string scenceName, CancellationToken ct = default)
    {
        var op = SceneManager.UnloadSceneAsync(scenceName);
        if (op == null) return;

        while (!op.isDone)
        {
            ct.ThrowIfCancellationRequested();
            await Task.Yield();
        }
    }

    private async Task Load(string sceneName, bool additive, bool activateOnLoad, CancellationToken ct = default)
    {
        _eventBus?.Publish(new SceneLoadStarted { Scence = sceneName, Additive = additive });

        var mode = additive ? LoadSceneMode.Additive : LoadSceneMode.Single;
        var op = SceneManager.LoadSceneAsync(sceneName, mode);

        if (op == null) return;

        op.allowSceneActivation = activateOnLoad;

        while (!op.isDone)
        {
            ct.ThrowIfCancellationRequested();
            _eventBus?.Publish(new SceneLoadProgress { Scene = sceneName, Progress = op.progress });

            await Task.Yield();
        }

        _eventBus?.Publish(new SceneLoadCompleted { Scence = sceneName, Additive = additive });
    }
}
