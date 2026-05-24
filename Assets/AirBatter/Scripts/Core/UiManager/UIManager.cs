using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : IUIManager
{

    private BaseScreen _curentScreen; 
    private Dictionary<string, BaseScreen> _screens = new();
    private IEventBus _eventBus;
    private GameObject _hud;


    public UIManager(IEventBus eventBus, List<BaseScreen> baseScreens, GameObject hud, BaseScreen curentScreen = null)
    {
        _eventBus = eventBus;
        _curentScreen = curentScreen;
        _hud = hud;

        if(_hud != null)
            GameObject.DontDestroyOnLoad(_hud);

        foreach (BaseScreen screen in baseScreens)
        {
            if(screen != null)
            {
                _screens.Add(screen.GetType().Name, screen);
            }
                
        }

        if(_curentScreen != null )
            _screens.Add(_curentScreen.name, _curentScreen);
    }

    public BaseScreen GetScreen<T>() where T : BaseScreen
    {
        var nameScreen = typeof(T).Name;

        if (!_screens.ContainsKey(nameScreen))
        {
            Debug.Log($"screen {nameScreen} not find");
            return null;
        }

        

        return _screens[nameScreen];
    }

    public void HideScreen<T>() where T : BaseScreen
    {
        var nameScreen = typeof(T).Name;

        if (!_screens.ContainsKey(nameScreen))
        {
            Debug.Log($"screen {nameScreen} not find");
            return;
        }

        _curentScreen = _screens[nameScreen];
        _curentScreen.Hide();

        _eventBus.Publish(new ScreenAction());
    }

    public void ShowScreen<T>() where T : BaseScreen
    {
        var nameScreen = typeof(T).Name;

        if (!_screens.ContainsKey(nameScreen))
        {
            Debug.Log($"screen {nameScreen} not find");
            return;
        }


        _curentScreen.Hide();
        _curentScreen = _screens[nameScreen];
        _curentScreen.Show();

        _eventBus.Publish(new ScreenAction());
    }

    public void Dispose()
    {
        _curentScreen = null;
        _screens = null;
    }

}
