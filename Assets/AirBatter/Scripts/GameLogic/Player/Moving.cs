using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float moveDistance = 2f;      // Расстояние за один свайп
    [SerializeField] private float moveSpeed = 10f;        // Скорость анимации движения
    [SerializeField] private float leftBorder = -8f;       // Левая граница
    [SerializeField] private float rightBorder = 8f;       // Правая граница

    [Header("Анимация")]
    [SerializeField] private Animator _animator;
    [SerializeField] private float _timeClip;

    

    [Header("Настройки свайпа")]
    [SerializeField] private float minSwipeDistance = 50f; // Минимальная длина свайпа (в пикселях)

    private Vector2 touchStartPos;
    private Vector3 targetPosition;
    private bool isSwiping = false;
    private bool isMoving = false;

    // Перечисление направлений свайпа
    private enum SwipeDirection { None, Left, Right }

    void Start()
    {
        // Стартовая позиция корабля
        targetPosition = transform.position;
    }

    void Update()
    {
        // Обрабатываем ввод
        HandleSwipeInput();

        // Плавно двигаем корабль к целевой позиции
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Если достигли цели, останавливаем движение
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                transform.position = targetPosition;
            }
        }
    }

    void HandleSwipeInput()
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

    // Определяем горизонтальное направление свайпа
    private SwipeDirection GetSwipeDirection(Vector2 delta)
    {
        // Игнорируем вертикальные свайпы, важна только горизонталь
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            return delta.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
        }
        return SwipeDirection.None;
    }

    // Перемещаем корабль в зависимости от свайпа
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
                return; // Ничего не делаем
        }
        


        // Ограничиваем движение границами
        newPosition.x = Mathf.Clamp(newPosition.x, leftBorder, rightBorder);

        // Если позиция изменилась, начинаем движение
        if (newPosition != targetPosition)
        {
            targetPosition = newPosition;
            isMoving = true;
        }
    }

    // Для отладки на компьютере (клик мышкой)
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
