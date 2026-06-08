using UnityEngine;
using UnityEngine.UI;

public class UiCards : BaseScreen
{

    private IEventBus _eventBus;

    private void Start()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        
    }

}
