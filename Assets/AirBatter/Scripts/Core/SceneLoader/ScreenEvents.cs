
public struct LoadScenceRequst: IEvent 
{
    public string Scence;
    public bool Addivite; 
    public bool ActivateOnLoad;
    public bool CanclePrevious;

    public LoadScenceRequst(string scence, bool addivite, bool activateOnLoad, bool canclePrevious)
    {
        Scence = scence;
        Addivite = addivite;
        ActivateOnLoad = activateOnLoad;
        CanclePrevious = canclePrevious;
    }
}

public struct UnloadScenceRequest : IEvent
{
    public string Scence;

    public UnloadScenceRequest(string scence)
    {
        Scence = scence;
    }
}

public struct ReloadActiveRequst : IEvent { }

public struct SceneLoadStarted : IEvent 
{
    public string Scence;
    public bool Additive;

    public SceneLoadStarted(string scence, bool additive)
    {
        Scence = scence;
        Additive = additive;
    }
}

public struct SceneLoadCompleted : IEvent
{
    public string Scence;
    public bool Additive;

    public SceneLoadCompleted(string scence, bool additive)
    {
        Scence = scence;
        Additive = additive;
    }
}

public struct SceneUnloadCompleted : IEvent 
{
    public string Scence;

    public SceneUnloadCompleted(string scence)
    {
        Scence = scence;
    }
}

public struct ScenceLoadCanceled : IEvent
{
    public string Scence;

    public ScenceLoadCanceled(string scence)
    {
        Scence = scence;
    }
}

public struct SceneLoadProgress : IEvent
{
    public string Scene;
    public float Progress; 

    public SceneLoadProgress(string scene, float progress)
    {
        Scene = scene;
        Progress = progress;
    }
}


