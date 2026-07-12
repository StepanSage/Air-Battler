using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : BaseScreen
{
    [SerializeField] private Slider _expBar;

    private IEventBus _eventBus;
    private IUIManager _ulManager;
    private IGamePause _gamePause;
    private BaseScreen _UiCards;
    private int _currentCountExp = 0;
    private int _amountRequiredExp = 10;
    [SerializeField] private int _countAddExp = 1;

    private void Start()
    {
        _ulManager = ServiceLocator.Instance.Get<IUIManager>();
        _UiCards = _ulManager.GetScreen<UiCards>();
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _eventBus.Subscribe<AddExperineceEvent>(AddExp);
        _eventBus.Subscribe<AddBonusExperineceEvent>(AddBonuseExperinece);
        _gamePause = ServiceLocator.Instance.Get<IGamePause>();


        ExpProgrees();

    }

    private void AddExp(AddExperineceEvent eventExp)
    {
        _currentCountExp += _countAddExp;

        ExpProgrees();
        LevelUP();
    }

    private void LevelUP()
    {
        if (_currentCountExp >= _amountRequiredExp)
        {
            _currentCountExp = 0;
            _amountRequiredExp *= 2;
            _UiCards.Show();
            _gamePause.Pause();
            
        }
    }

    private void AddBonuseExperinece(AddBonusExperineceEvent addBonusExperineceEvent)
    {
        _countAddExp += addBonusExperineceEvent.BonusExperience;
    }

    private void ExpProgrees() => _expBar.value = (float)_currentCountExp / _amountRequiredExp;
        
}

