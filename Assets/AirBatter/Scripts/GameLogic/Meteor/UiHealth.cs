using UnityEngine;
using TMPro;
using DG.Tweening;

public class UiHealth : MonoBehaviour
{
    [SerializeField] private Damageable _health;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Sprite[] _sprites;
    [Space(10)]
    [SerializeField] private float _duration = 0.1f; 
    [SerializeField] private Vector2 _targetScale = new Vector2(0.5f, 0.5f); 

    private Vector3 _originalScale;
    private SpriteRenderer spriteRenderer;
    private bool _isAnimating = false;

    private void Start()
    {
        _health.OnHealthChangeRange += ChangeHealth;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ChangeUI(_health.MaxHealth);
        _originalScale = transform.localScale;
    }

    private void ChangeHealth(int currentHealth, int maxHealth)
    {
        ChangeUI(currentHealth);
        ChangeIcon(currentHealth, maxHealth);
       
    }

    private void ChangeUI(int valueHealht)
    {
        _healthText.text = valueHealht.ToString();
       
    }

    private void ChangeIcon(int CurrentHealth, int MaxHealth)
    {
        float value = (float)CurrentHealth / (float)MaxHealth;

        if (value == 1)
            spriteRenderer.sprite = _sprites[0];
        else if (value >= 0.8f)
            spriteRenderer.sprite = _sprites[1];
        else if (value >= 0.5f)
            spriteRenderer.sprite = _sprites[2];
        else if (value >= 0.2f)
            spriteRenderer.sprite = _sprites[3];
    }

    


    private void OnDestroy()
    {
        _health.OnHealthChangeRange -= ChangeHealth;
    }
}
