using UnityEngine;

[RequireComponent(typeof(DealDamage))]
public class CollisionHandlerDamage : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _isPlaySFX = false;

    protected DealDamage _deal;

    private IAudioManager _audioManager;
   
    private void Start()
    {
        _deal = GetComponent<DealDamage>();
        _audioManager = ServiceLocator.Instance.Get<IAudioManager>();
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((_targetLayer.value & (1 << collision.gameObject.layer)) != 0)
            TryDealDamage(collision.gameObject);  
    }

    private void TryDealDamage(GameObject target)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();

        damageable?.TakeDamage(_deal.Damage);

        if (_isPlaySFX == true)
        {
            _audioManager.PlaySfx(_audioClip, 1f);
            Debug.Log("Music play now");
        }
           

        DestroyOnHit();
               
    }

    protected virtual void DestroyOnHit()
    {
        if (_deal.DestroyOnHit)
            _deal.gameObject.SetActive(false);
    }
    
}
