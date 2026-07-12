using UnityEngine;

public abstract class Ability : MonoBehaviour
{

    [field: SerializeField] public AbilityConfig AbilityConfig { get; private set; }

    protected IEventBus _eventBus;
    protected IAudioManager _audioManager;
   

    private void Start() => Initialize();

    protected virtual void Initialize()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _audioManager = ServiceLocator.Instance.Get<IAudioManager>();
    }

    public abstract void Useing();
}
