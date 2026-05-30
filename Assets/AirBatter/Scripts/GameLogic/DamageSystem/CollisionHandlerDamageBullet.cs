using UnityEngine;

public class CollisionHandlerDamageBullet : CollisionHandlerDamage
{
    private PoolObject<GameObject> _poolObject;

    public void Initialaze(PoolObject<GameObject> poolObject)
    {
        _poolObject = poolObject;
    }

    protected override void DestroyOnHit()
    {
        if(_deal.DestroyOnHit)
        {
            _deal.gameObject.SetActive(false);
        }
    }
}
