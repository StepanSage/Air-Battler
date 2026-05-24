using System;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour
{
    [SerializeField] protected bool _hideOnStart = false;

    public  Action OnShow { get; private set;}
    public  Action OnHide { get; private set;} 
    
    public void Initialaze()
    {
        if(_hideOnStart) Hide();
    }


    public virtual void Show()
    {
        OnShow?.Invoke();
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        OnHide?.Invoke();
        gameObject.SetActive(false);
    }

    public virtual void Close()
    {

    }
}
