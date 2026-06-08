using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float moveDistance = 2f;      
    [SerializeField] private float moveSpeed = 10f;        
    [SerializeField] private float leftBorder = -8f;      
    [SerializeField] private float rightBorder = 8f;      

    [Header("Анимация")]
    [SerializeField] private Animator _animator;
    [SerializeField] private float _timeClip;

    
    [Header("Настройки свайпа")]
    [SerializeField] private float minSwipeDistance = 50f;

    [Header("SFX")]
    [SerializeField] private AudioClip _audioClip;

    private Vector2 touchStartPos;
    private Vector3 targetPosition;
    private bool isSwiping = false;
    private bool isMoving = false;
    private IAudioManager _audioManager;

    private enum SwipeDirection { None, Left, Right }

    private void Start()
    {
        targetPosition = transform.position;
        _audioManager = ServiceLocator.Instance.Get<IAudioManager>();
    }

    void Update()
    {
        
        HandleSwipeInput();

        
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                transform.position = targetPosition;
            }
        }
    }

    private void HandleSwipeInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Начало касания
                    touchStartPos = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Ended:
                    if (isSwiping)
                    {
                        // Вычисляем вектор свайпа
                        Vector2 swipeDelta = touch.position - touchStartPos;

                        // Проверяем, что свайп достаточно длинный
                        if (swipeDelta.magnitude >= minSwipeDistance)
                        {
                            // Определяем направление
                            SwipeDirection direction = GetSwipeDirection(swipeDelta);

                            // Двигаем корабль
                            MoveShip(direction);
                            
                        }
                        isSwiping = false;
                    }
                    break;

                case TouchPhase.Canceled:
                    isSwiping = false;
                    break;
            }
        }
    }

    
    private SwipeDirection GetSwipeDirection(Vector2 delta)
    {
        
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            return delta.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
        }
        return SwipeDirection.None;
    }

   
    private void MoveShip(SwipeDirection direction)
    {
        Vector3 newPosition = targetPosition;

        switch (direction)
        {
            case SwipeDirection.Left:
                StartCoroutine(AnimationPlay("TurnLT", _timeClip));
                newPosition.x -= moveDistance;
                Debug.Log("⬅️ Свайп влево - двигаем корабль влево");
                break;

            case SwipeDirection.Right:
                StartCoroutine(AnimationPlay("TurnRT", _timeClip));
                newPosition.x += moveDistance;
                Debug.Log("➡️ Свайп вправо - двигаем корабль вправо");
                break;

            default:
                return; 
        }

        _audioManager.PlaySfx(_audioClip, 0.5f);


        newPosition.x = Mathf.Clamp(newPosition.x, leftBorder, rightBorder);

       
        if (newPosition != targetPosition)
        {
            targetPosition = newPosition;
            isMoving = true;
        }
    }


    void OnGUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
            isSwiping = true;
        }

        if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            Vector2 swipeDelta = (Vector2)Input.mousePosition - touchStartPos;
            if (swipeDelta.magnitude >= minSwipeDistance)
            {
                SwipeDirection direction = GetSwipeDirection(swipeDelta);
                MoveShip(direction);
            }
            isSwiping = false;
        }
    }

    private IEnumerator AnimationPlay(string animationName, float timeClip)
    {
         _animator.SetBool(animationName, true);
         yield return new WaitForSeconds(timeClip);
         _animator.SetBool(animationName, false);
    }
}
