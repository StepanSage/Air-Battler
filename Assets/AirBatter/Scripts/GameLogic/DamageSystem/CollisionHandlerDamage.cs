using UnityEngine;

[RequireComponent(typeof(DealDamage))]
public class CollisionHandlerDamage : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    protected DealDamage _deal;
   
    private void Start() => _deal = GetComponent<DealDamage>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((_targetLayer.value & (1 << collision.gameObject.layer)) != 0)
            TryDealDamage(collision.gameObject);  
    }

    private void TryDealDamage(GameObject target)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();

        damageable?.TakeDamage(_deal.Damage);

        DestroyOnHit();
               
    }

    protected virtual void DestroyOnHit()
    {
        if (_deal.DestroyOnHit)
            _deal.gameObject.SetActive(false);
    }
    
}
