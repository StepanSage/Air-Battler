using System.Collections.Generic;
using UnityEngine;

namespace Assets.AirBatter.Scripts.GameLogic.Shop
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private GameDataManager _dataManager;
        [SerializeField] private ShopView _shopView;
        [SerializeField] private Component _buyProductComponent;
        [Space(10)]
        [SerializeField] private GameObject _playCanvas;
        [SerializeField] private GameObject _settingCanvas;
        [Space(10)]
        [SerializeField] private AudioClip _button;
        [SerializeField] private AudioClip _buy;
        [SerializeField] private AudioClip _refusal;

        private ISaveSystem _saveSystem;
        private DataProduct _selectAirplane;
        private DataProduct _displayAirplane; 
        private int index = 0;
        private IEventBus _eventBus;
        private IBuyProduct _buyProduct;
        private IAudioManager _audioManager;

        private void OnEnable() 
            => StartDisplay();

        private void Start()
        {
            Initialized();
            RigisterEvent();
            StartDisplay();
        }

        private void Initialized()
        {
            _eventBus = ServiceLocator.Instance.Get<IEventBus>();
            _buyProduct = _buyProductComponent.GetComponent<IBuyProduct>();
            _audioManager = ServiceLocator.Instance.Get<IAudioManager>();
            _saveSystem = ServiceLocator.Instance.Get<ISaveSystem>();
            if(_buyProduct == null) 
                Debug.LogError("Интерфейс IBuyProduct не найтден");


        }

        private void RigisterEvent()
        {
            _shopView.OnBuy += Buy;
            _shopView.OnSelect += Select;
            _shopView.OnSwapLeft += SwapLeft;
            _shopView.OnSwapRight += SwapRight;
            _shopView.OnClose += Close ;
        }

        private void Buy()
        {
            if(_buyProduct.Buy(_displayAirplane.Price) == true)
            {
                SoundPlay(_buy);
                Debug.Log("операция по покупеке самолета прошла успешно");
                _eventBus.Publish<BuyAirplaneEvent>(new(_displayAirplane, true));
                var Save = _saveSystem.GetData();
                Save.ListID.Add(_displayAirplane.ID);
                _saveSystem.Save();
            }
            else
            {
                SoundPlay(_refusal);
                Debug.Log("недостаточно средств");
            } 
        }

        private void Select()
        {
            _selectAirplane = _displayAirplane;
            _eventBus.Publish<SelectAirplaneEvent>(new(_selectAirplane));
            _saveSystem.GetData().DesplayAirplanIndex =_selectAirplane.ID;
            _saveSystem.Save();
            Close();
        }

        private void SwapLeft()
        {
            index--;
            Swap();
        }

        private void SwapRight()
        {
            index++;
            Swap();
        }

        private void Swap()
        {
            SoundPlay(_button);
            CalulateIndex();
            _displayAirplane = _dataManager.Airplanes[index];
            CheckAndDisplayAirplane();     
        }

        private void CheckAndDisplayAirplane()
        {
            bool isPurchased = _saveSystem.GetData().ListID.Contains(_displayAirplane.ID);

            if (isPurchased)
            {
                Display(_displayAirplane.ID, true);
            }
            else
            {
                Display(_displayAirplane.ID, false);
            }
            
        }
        private void Close()
        {
            SoundPlay(_button);
            StartDisplay();
            gameObject.SetActive(false);
            _playCanvas?.SetActive(true);
            _settingCanvas?.SetActive(true);
            _saveSystem.Save();
        }
        private void StartDisplay()
        {
            Display(0, true);
            index = 0;
        }
        private void CalulateIndex()
        {
            int minValue = 0;
            
            if (index < minValue)
                index = _dataManager.Airplanes.Count-1;
            else if (index >= _dataManager.Airplanes.Count)
                index = 0;
        }
        private void Display(int ID, bool IsBuy)
        {
            _displayAirplane = _dataManager.Airplanes[ID];
            _eventBus?.Publish<SwapAirPlaneEvent>(new(_displayAirplane, IsBuy));
        }

        private void SoundPlay(AudioClip audioClip)
        {
            _audioManager?.PlaySfx(audioClip);
        }

    }
}

public struct SelectAirplaneEvent: IEvent
{
    public DataProduct SelcetAirplane { get; private set; }

    public SelectAirplaneEvent(DataProduct selcetAirplane)
    {
        SelcetAirplane = selcetAirplane;
    }
}

public struct SwapAirPlaneEvent : IEvent
{
    public bool IsBuy;
    public DataProduct DataProduct;


    public SwapAirPlaneEvent(DataProduct dataProduct, bool isBuy)
    {
        IsBuy = isBuy;
        DataProduct = dataProduct;
    }
}

public struct BuyAirplaneEvent : IEvent
{
    
    public bool IsBuy;
    public DataProduct DataProduct;
    public BuyAirplaneEvent(DataProduct data, bool IsBuy)
    {
        this.IsBuy = IsBuy;
        DataProduct = data;
    }
}


