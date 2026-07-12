using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityTest : MonoBehaviour
{
    [SerializeField] private Button _bt;

    private IEventBus _eventBus;
    private void Start()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _bt.onClick.AddListener(AddBounce);
    }

    void Update()
    {
        //if (Application.isEditor && Input.GetKeyDown(KeyCode.Alpha1))
        //{
            
        //}
    }

    private void AddBounce()
    {
        Debug.Log("=======BONUSE========");
        _eventBus?.Publish<AddBonusExperineceEvent>(new(1000000));
    }
}
