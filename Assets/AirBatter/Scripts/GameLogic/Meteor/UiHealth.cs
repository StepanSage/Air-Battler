using UnityEngine;
using TMPro;


public class UiHealth : MonoBehaviour
{
    [SerializeField] private Damageable _health;
    [SerializeField] private TMP_Text _healthText;

    private void Start()
    {
        _health.OnHealthChange += ChangeHealth;
        ChangeUI(_health.MaxHealth);
        
    }

    private void ChangeHealth(int currentHealth, int maxHealth)
    {
        ChangeUI(currentHealth);
    }

    private void ChangeUI(int valueHealht)
    {
        _healthText.text = valueHealht.ToString();
    }


    private void OnDestroy()
    {
        _health.OnHealthChange -= ChangeHealth;
    }
}
