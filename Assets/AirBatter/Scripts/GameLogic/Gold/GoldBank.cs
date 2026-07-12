using UnityEngine;

public class GoldBank : MonoBehaviour
{
    [SerializeField] private IGoldView _visualGold;

    public uint Gold { get; private set; }

    private IEventBus _eventBus;

    private void Start()
    {
        _visualGold = GetComponentInChildren<IGoldView>();
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _eventBus?.Subscribe<AddGoldEvent>(Add);
        _eventBus?.Subscribe<RemoveGoldEvent>(Remove);
        UpdateUI();
    }

   

    public void Add(AddGoldEvent addge)
    {
        Gold += addge.Add;
        UpdateUI();
    }

    public void Remove(RemoveGoldEvent rge)
    {
        if (rge.Remove > Gold) return; 

        Gold -= rge.Remove;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _visualGold.RendererGold(Gold);
    }

}
