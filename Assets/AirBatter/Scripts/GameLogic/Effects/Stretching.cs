using System.Collections;
using UnityEngine;

public class Stretching : MonoBehaviour
{
    [SerializeField] private Transform _object;
    [SerializeField] private Vector2 _stretching;
    [SerializeField] private float _duration = 0.3f;

    private float _valueLerp;
    private Vector2 _normalScale;

    
    private void OnEnable()
    {

        if (_object == null)
            _object = gameObject.GetComponent<Transform>();

        _normalScale = _object.transform.localScale;
        StartCoroutine(LerpOverTime());
    }


    private void Stretch(float value)
    {
        _object.localScale = Vector2.Lerp(_stretching, _normalScale, value);
    }


    private IEnumerator LerpOverTime()
    {
        float currenTime = 0;

        while(_duration > currenTime)
        {
            _valueLerp = currenTime / _duration;
            Stretch(_valueLerp);
            currenTime += Time.deltaTime;
            yield return null;
        }
    }
}
