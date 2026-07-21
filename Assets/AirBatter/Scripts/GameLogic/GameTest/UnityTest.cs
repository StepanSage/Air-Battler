using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityTest : MonoBehaviour
{
    [SerializeField] private Button _addExp;
    [SerializeField] private Button _addGold;

    private IEventBus _eventBus;
    private void Start()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _addExp.onClick.AddListener(AddBounce);
        _addGold.onClick.AddListener(AddGold);
    }
    
    private void AddBounce()
    {
        Debug.Log("=======BONUSE ADD EXP========");
        _eventBus?.Publish<AddBonusExperineceEvent>(new(1000000));
    }
    private void AddGold()
    {
        Debug.Log("=======BONUSE ADD GOLD========");
        _eventBus.Publish<AddGoldEvent>(new(100));
    }


}
