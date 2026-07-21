using UnityEngine;

public class GoldBank : MonoBehaviour, IBuyProduct
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

    public bool Buy(uint price)
    {
        price = price < 0 ? 0 : price;

        if((int)Gold - (int)price > 0)
        {
            Gold = Gold - price;
            UpdateUI();
            return true;
        }
        else
        {
            return false;
        }
    }
}
