using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private bool _destroyOnHit = true;

    public int Damage => _damage;
    public bool DestroyOnHit => _destroyOnHit;

    public virtual void SetDamage(int damage)
    {
        _damage = damage;
        Debug.Log("Улучшение произошло");
    }
}
