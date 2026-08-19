using System;
using UnityEngine;

public class GlobalGameState : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _Game;
    [SerializeField] private LodingShutter _lodingShutter;

    private void Awake()
    {
        Menu();
    }
    
    public void Set(GameState state)
    {
        if(GameState.Menu == state)
            OpenMenu();
    }

    public void Play()
    {
        Loading(() => Game());
    }

    private void OpenMenu()
    {
        Loading(() => Menu());
    }

    private void Loading(Action action )
    {
        _lodingShutter.Launch(action);
    }

    private void Menu()
    {
        _menu.SetActive(true);
        _Game.SetActive(false);
    }
    private void Game()
    {
        _Game.SetActive(true);
        _menu.SetActive(false);
    }

}

public enum GameState
{
    None,
    Menu,
    Game,
    Setting
}
