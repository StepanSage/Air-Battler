using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerInstaller : Installer
{
    [SerializeField] private GameObject _hud;
    [SerializeField] private BaseScreen _startScreen = null;
    [SerializeField] private List<BaseScreen> _allScreen;

    private IEventBus _eventBus;
    private IUIManager _instance;

    public override void Install(ServiceLocator serviceLocator)
    {
        _eventBus = serviceLocator.Get<IEventBus>();
        _instance = new UIManager(_eventBus, _allScreen, _hud, _startScreen);
        serviceLocator.Rigister<IUIManager>(_instance);


    }

    public override void Uninstall(ServiceLocator serviceLocator)
    {
        serviceLocator.UnRigister<IUIManager>(_instance);
        (_instance as UIManager)?.Dispose();
        _instance = null;

    }
}
