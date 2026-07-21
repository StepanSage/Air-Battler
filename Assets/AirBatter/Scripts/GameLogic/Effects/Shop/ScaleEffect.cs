using DG.Tweening;
using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
    [SerializeField] private Vector2 _endScale = new Vector2(0.9f,0.9f);
    [SerializeField] private float _timeScale = 0.1f;

    private Vector2 _startScale;

    private void Start()
    {
        _startScale = transform.localScale;
    }

    public void Launch()
    {
        transform.DOScale(_endScale, _timeScale);
        Invoke(nameof(UnScale), _timeScale);
    }

    private void UnScale()
    {
        transform.DOScale(_startScale, 0);
    }

    public float GetTimeScale() => _timeScale;
}
