using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : BaseScreen
{
    [SerializeField] private Slider _expBar;

    private IEventBus _eventBus;
    private int _currentCountExp = 0;
    private int _amountRequiredExp = 10;
    private int _countAddExp = 1;

    private void Start()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _eventBus.Subscribe<ExpEvent>(AddExp);

        ExpProgrees();

        Debug.Log($"value bar{_expBar.value}");
    }

    private void AddExp(ExpEvent eventExp)
    {
        _currentCountExp += _countAddExp;

        ExpProgrees();
        LevelUP();

        Debug.Log($"exp {_currentCountExp}");
    }

    private void LevelUP()
    {
        if (_currentCountExp >= _amountRequiredExp)
        {
            _currentCountExp = 0;
            _amountRequiredExp *= 2;

            // логика появление карточек улучшений на экране
        }
    }

    private void ExpProgrees() => _expBar.value = (float)_currentCountExp / _amountRequiredExp;
        
}
