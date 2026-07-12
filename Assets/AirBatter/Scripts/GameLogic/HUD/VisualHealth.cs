using UnityEngine;
using UnityEngine.UI;

public class VisualHealth : MonoBehaviour
{
    [SerializeField] private Image[] _allHealth;
   
    private IEventBus _eventBus;
    private int _countActiveHealth = 1;
    private int _countAllHealth;

    private void Start()
    {
        if (_allHealth == null) return;

        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _eventBus?.Subscribe<ChangeRenderingHealth>(ChangeActiveHealth);

        _countAllHealth = _allHealth.Length;
        _countActiveHealth = Mathf.Clamp(_countActiveHealth, 0, _countAllHealth);

        DrowUI();

    }

    private void ChangeActiveHealth(ChangeRenderingHealth crh)
    {
        _countActiveHealth += crh.countRenderHealth;
        _countActiveHealth = Mathf.Clamp(_countActiveHealth, 0, _countAllHealth);

        DrowUI();
    }

    private void DrowUI()
    {
        for (int i = 0; i < _countAllHealth; i++)
        {
            bool shouldBeActive = i < _countActiveHealth;
            _allHealth[i].gameObject.SetActive(shouldBeActive);
        }
    }
}

public struct ChangeRenderingHealth: IEvent
{
    public int countRenderHealth;

    public ChangeRenderingHealth(int countRenderHealth)
    {
        this.countRenderHealth = countRenderHealth;
    }
}
