using UnityEngine;

public class FlihgtBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed  = 5;

    private IEventBus _eventBus;

    private void Start()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _eventBus.Subscribe<OnUpShootSpeedEvent>(AddSpeed);
    }

    private void Update() => MoveBullet(); 

    private void MoveBullet() => transform.Translate(transform.up * _bulletSpeed * Time.deltaTime);

    public void AddSpeed(OnUpShootSpeedEvent onUpShootSpeedEvent)
    {
        _bulletSpeed += onUpShootSpeedEvent.Speed;
    }

}
