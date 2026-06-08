using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseInstaller : Installer
{
    private IGamePause _instance;

    public override void Install(ServiceLocator serviceLocator)
    {
        _instance = new GamePause();
        serviceLocator.Rigister<IGamePause>(_instance);
    }

    public override void Uninstall(ServiceLocator serviceLocator)
    {
        _instance = null;
        serviceLocator.UnRigister<IGamePause>(_instance);
    }
}
