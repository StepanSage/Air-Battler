using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DealDamage))]
public class CollisionHandlerDamage : MonoBehaviour
{
    private DealDamage _buller;

    [SerializeField] private LayerMask _targetLayer;

    private void Start()
    {
        _buller = GetComponent<DealDamage>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((_targetLayer.value & (1 << collision.gameObject.layer)) != 0)
        {       
            TryDealDamage(collision.gameObject);
        }          
    }

    private void TryDealDamage(GameObject target)
    {
       IDamageable damageable = target.GetComponent<IDamageable>();

        if(damageable != null)
            damageable.TakeDamage(_buller.Damage);

        if (_buller.DestroyOnHit == true)
            Destroy(_buller.gameObject);
    }
}
