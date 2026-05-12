using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerningTouch : MonoBehaviour
{
    private Vector2 _startTouchPos;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch(touch.phase)
            {
                case TouchPhase.Began:
                    _startTouchPos = touch.position;
                    
                    break;

                case TouchPhase.Ended:
                   
                    Vector2 delta = touch.position - _startTouchPos;
                    GetSwipeDiraction(delta);
                    
                    break;
            }

           
        }
    }

    private void GetSwipeDiraction(Vector2 delta)
    {
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
                Debug.Log("право");
            else
                Debug.Log("Лево");
        }
        else
        {
            if (delta.y > 0)
                Debug.Log("вверх");
            else
                Debug.Log("низ");
        }
    }
}
