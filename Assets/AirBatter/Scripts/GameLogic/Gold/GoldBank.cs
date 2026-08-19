using UnityEngine;

public class GoldBank : MonoBehaviour, IBuyProduct
{
    [SerializeField] private IGoldView _visualGold;

    public uint Gold { get; private set; }

    private IEventBus _eventBus;
    private ISaveSystem _saveSystem;

    private void Awake()
    {
        _visualGold = GetComponentInChildren<IGoldView>();
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _saveSystem = ServiceLocator.Instance.Get<ISaveSystem>();
    }
    private void OnEnable()
    {
        _eventBus?.Subscribe<AddGoldEvent>(Add);
        _eventBus?.Subscribe<RemoveGoldEvent>(Remove);
    }

    private void OnDisable()
    {
        _eventBus?.Unsubscribe<AddGoldEvent>(Add);
        _eventBus?.Unsubscribe<RemoveGoldEvent>(Remove);
    }

    private void Start()
    {
        Gold = _saveSystem.GetData().CountGold;
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
        SaveGold();
    }

    public bool Buy(uint price)
    {
        price = price < 0 ? 0 : price;

        if((int)Gold - (int)price >= 0)
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

    private void SaveGold()
    {
        _saveSystem.GetData().CountGold = Gold;
        _saveSystem.Save();
    }
}
