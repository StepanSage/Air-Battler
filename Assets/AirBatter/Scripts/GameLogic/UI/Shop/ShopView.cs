using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.AirBatter.Scripts.GameLogic.Shop
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private Button _select;
        [SerializeField] private Button _buy;
        [SerializeField] private Button _swapLeft;
        [SerializeField] private Button _swapRight;
        [SerializeField] private Button _close;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _textPrice;

        private IEventBus _eventBus;
        private Action _bufferEvent;
        private bool _isAnimation = false;
        
        public Action OnSelect;
        public Action OnBuy;
        public Action OnSwapLeft;
        public Action OnSwapRight;
        public Action OnClose;

        private void Awake()
        {
            Initialize();
            RegisterEvent();
        }
        
        private void Initialize()
        {
            _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        }

        private void RegisterEvent()
        {
            _eventBus?.Subscribe<SwapAirPlaneEvent>(ChangeUI);
            _eventBus?.Subscribe<BuyAirplaneEvent>(ChangeButton);

            _select?.onClick.AddListener(() => ExecuteWithAnimation(_select.gameObject, OnSelect));
            _buy?.onClick.AddListener(() => ExecuteWithAnimation(_buy.gameObject, OnBuy));
            _close?.onClick.AddListener(() => ExecuteWithAnimation(_close.gameObject, OnClose));
            _swapLeft?.onClick.AddListener(() => OnSwapLeft?.Invoke());
            _swapRight?.onClick.AddListener(() => OnSwapRight?.Invoke());
        }


        private void ExecuteWithAnimation(GameObject go, Action action )
        {
            ScaleEffect scaleEffect = go.GetComponent<ScaleEffect>();

            if (go == null || action == null || scaleEffect == null || _isAnimation) return;

            scaleEffect.Launch();
            _bufferEvent = action;
            _isAnimation = true;

            Invoke(nameof(SendEvent), scaleEffect.GetTimeScale() + 0.1f);
        }

        private void SendEvent()
        {
            _bufferEvent?.Invoke();
            _isAnimation = false;
            _bufferEvent = null;
        }

        private void ChangeUI(SwapAirPlaneEvent swap)
        {
            ChangeStateButton(swap.IsBuy);
            _icon.sprite = swap.DataProduct.Icon;
            _textPrice.text = swap.DataProduct.Price.ToString();
        }
        private void ChangeButton(BuyAirplaneEvent buy)
        {
            _buy?.GetComponent<ScaleEffect>().Launch();
            ChangeStateButton(buy.IsBuy);

        }
        private void ChangeStateButton(bool IsBuy)
        {
            if (IsBuy == true)
            {
                _select.gameObject.SetActive(true);
                _buy.gameObject.SetActive(false);
            }
            else
            {
                _select.gameObject.SetActive(false);
                _buy.gameObject.SetActive(true);

            }
        }

       
    }
}