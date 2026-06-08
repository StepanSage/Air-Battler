using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BG : MonoBehaviour
{
    [SerializeField] private Transform[] _bg;
    [Range(0,20f)][SerializeField] private float _scrollSpeed = 1.0f;

    private Vector2[] _startPosition;
    private float _bgLength; 

    private void Start()
    {
        

        var colider = _bg[0].GetComponent<Collider>();

        if (colider != null)
        {
            _bgLength = colider.bounds.size.y;
        }
        else
        {
            var sp = _bg[0].GetComponent<SpriteRenderer>();
            _bgLength = sp.bounds.size.y;
        }


        
        for (int i = 0; i < _bg.Length; i++)
        {
            Vector3 pos = _bg[i].position;
            pos.y = i * _bgLength;
            _bg[i].position = pos;
        }

    }

    
    private void Update()
    {
        for (int i = 0; i < _bg.Length; i++)
        {
            _bg[i].Translate(Vector3.down * _scrollSpeed * Time.deltaTime);

          
            if (_bg[i].position.y <= -_bgLength + 0.01f)
            {
             
                float rightmostY = GetRightmostPosition();

                
                Vector3 newPos = _bg[i].position;
                newPos.y = rightmostY + _bgLength;
                _bg[i].position = newPos;
            }
        }
    }

    private float GetRightmostPosition()
    {
        float maxY = _bg[0].position.y;
        foreach (Transform bg in _bg)
        {
            if (bg.position.y > maxY)
                maxY = bg.position.y;
        }
        return maxY;
    }
}
