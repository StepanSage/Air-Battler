using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButtonView : MonoBehaviour
{
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private GlobalGameState _gameState;
    [SerializeField] private AudioClip _buttonClip;
    [Space]
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _play;
    [SerializeField] private GameObject _setting;

    
    private IAudioManager _audioManager;
    private GameState _currentState;

    private void Awake()
    {
        _audioManager = ServiceLocator.Instance.Get<IAudioManager>();
    }

    private void Start()
    {
        _shopButton.onClick.AddListener(() => ButtonStateGame(_shopButton.gameObject, GameState.Menu));
        _settingButton.onClick.AddListener(() => ButtonStateGame(_settingButton.gameObject, GameState.Setting));
    }

    private void ButtonStateGame(GameObject Button, GameState state)
    {
        ScaleEffect effect = Button.GetComponent<ScaleEffect>();

        if (Button == null && effect == null) return;

        _currentState = state;

        effect.Launch();
        Sound();

        Invoke("ChangeGameState", 0.5f);

    }

    private void ChangeGameState()
    {
        if(GameState.Menu == _currentState)
        {
            _shop.SetActive(true);
            _play.SetActive(false);
            _setting.SetActive(false);
        }
        else if(GameState.Setting == _currentState)
        {
            _shop.SetActive(false);
            _play.SetActive(false);
            _setting.SetActive(true);
        }
    }

    private void Sound()
    {
        _audioManager?.PlaySfx(_buttonClip);
    }
}
