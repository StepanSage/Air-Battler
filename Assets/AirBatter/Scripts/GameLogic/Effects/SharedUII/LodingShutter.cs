using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LodingShutter : MonoBehaviour
{
    [SerializeField] private Image _lodingShutter;
    [SerializeField, Range(0, 2)] private float _timeFade;

    private Action _callBack;
    

    public void Launch(Action callBack)
    {
        _callBack = callBack;
        gameObject.SetActive(true);
        _lodingShutter.DOFade(1, 1f);
        Invoke("ResetFade", 1f);
    } 

    private void ResetFade()
    {
        _lodingShutter.DOFade(0f, 1f);
        _callBack?.Invoke();
    } 
}
