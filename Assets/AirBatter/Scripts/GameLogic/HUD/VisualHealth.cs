using UnityEngine;
using UnityEngine.UI;

public class VisualHealth : MonoBehaviour
{
    [SerializeField] Damageable _player;
    [SerializeField] private Image[] _allHealth;
    [SerializeField] private int _countActiveHealth = 3;

    private int countAllHealth; 

    private void Start()
    {
        _player.OnChangHealth += ChangeActiveHealth;

        if (_allHealth == null) return;

        countAllHealth = _allHealth.Length;
        _countActiveHealth = Mathf.Clamp(_countActiveHealth, 0, countAllHealth);

        DrowUI();

    }

    private void ChangeActiveHealth()
    {
        _countActiveHealth--;

        DrowUI();
    }

    private void DrowUI()
    {
        for (int i = 0; i < countAllHealth; i++)
        {
            bool shouldBeActive = i < _countActiveHealth;
            _allHealth[i].gameObject.SetActive(shouldBeActive);
        }
    }

   

    
    
}
