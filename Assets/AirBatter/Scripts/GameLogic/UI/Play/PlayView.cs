using UnityEngine;
using UnityEngine.UI;

public class PlayView : MonoBehaviour
{
    [SerializeField] private Image _airplanePlayer;
    [SerializeField] private GameDataManager _gdm;

    private IEventBus _eventBus;
    private ISaveSystem _saveSystem;

    private void Awake()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _saveSystem = ServiceLocator.Instance.Get<ISaveSystem>();
    }

    private void Start()
    {
        _eventBus.Subscribe<SelectAirplaneEvent>(ChangeIcon);
        var index = _saveSystem.GetData().DesplayAirplanIndex;
        _airplanePlayer.sprite = _gdm.Airplanes[index].Icon;
    }

    private void ChangeIcon(SelectAirplaneEvent select)
    {
        _airplanePlayer.sprite = select.SelcetAirplane.Icon;
    }

    
}
