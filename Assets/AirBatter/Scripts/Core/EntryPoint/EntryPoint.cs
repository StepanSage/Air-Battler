using System;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class EntryPoint : MonoBehaviour
{
    [Header("Start Flow")]
    [SerializeField] private bool _dontDestroyOnLoad = false;
    [SerializeField] private string _nameScence;

    private IEventBus _eventBus;

    private void Awake() 
    {
       if (_dontDestroyOnLoad) DontDestroyOnLoad(this); 
    }

    private void OnEnable()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();

        if(_eventBus == null)
        {
            Debug.LogError($"Event bus is null in EntryPoint when Enable");
            enabled = false;
            return;
        }


        _eventBus.Subscribe<SceneLoadCompleted>(OnScenceLoad);
    }


    private void Start()
    {
        _eventBus.Publish(new LoadScenceRequst(
            scence: _nameScence,
            addivite: false,
            activateOnLoad : true,
            canclePrevious : true
            )) ;
    }

    private void OnDisable()
    {
        if (_eventBus != null)
            _eventBus.Unsubscribe<SceneLoadCompleted>(OnScenceLoad);
    }

    private void OnScenceLoad(SceneLoadCompleted evt)
    {
        if (!string.Equals(evt.Scence, _nameScence))
            return;


    }
}
