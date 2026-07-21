using UnityEngine;
using UnityEngine.UI;

public class PlayView : MonoBehaviour
{
    [SerializeField] private Image _airplanePlayer;

    private IEventBus _eventBus;

    private void Start()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _eventBus.Subscribe<SelectAirplaneEvent>(ChangeIcon);
    }

    private void ChangeIcon(SelectAirplaneEvent select)
    {
        _airplanePlayer.sprite = select.SelcetAirplane.Icon;
    }

    
}
