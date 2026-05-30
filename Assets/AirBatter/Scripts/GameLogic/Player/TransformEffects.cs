using UnityEngine;
using DG.Tweening;

public class TransformEffects : MonoBehaviour
{
    
    [SerializeField] private Transform _target;

    [Header("Squash"), Space(5)]
    [SerializeField] private bool _enabledSquash = true;
    [SerializeField] private Vector2 _squashScale = new Vector2(1, 1);
    [SerializeField] private float _returnDurationSquash = 0.15f;

    [Header("ShiftingBack"), Space(5)]
    [SerializeField] private bool _enabledShiftingBack = true;
    [SerializeField] private float _shiftingBackY;
    [SerializeField] private float _returnDurationShifting = 0.15f;
    [SerializeField] private float _startPositionY;
    

    private IEventBus _eventBus;

    private void OnEnable()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _eventBus.Subscribe<OnShotEvent>(OnShot);
    }

    private void Start()
    {
        _target = gameObject.transform;
    }

    private void OnShot(OnShotEvent onShotEvent)
    {
        Squash();
        ShiftingBack();
    }

    private void Squash()
    {
        if (!_enabledSquash) return;

        _target.localScale = _squashScale;
        transform.DOScale(Vector2.one, _returnDurationSquash);
    }

    private void ShiftingBack()
    {
        if (!_enabledShiftingBack) return;

        _target.localPosition = new Vector2(transform.position.x, _shiftingBackY);
        transform.DOMoveY(_startPositionY, _returnDurationShifting);
    }
}

public struct OnShotEvent : IEvent
{

}
