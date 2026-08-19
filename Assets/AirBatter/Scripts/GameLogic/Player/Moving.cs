using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float _smoothSpeed = 8f; // Скорость следования

    [Header("Настройки спавна")]
    [SerializeField] private Vector2 _spawnPosition = new Vector2(0, 0); // Точка спавна
    [SerializeField] private bool _spawnAtStart = true; // Спавниться при старте

    private Vector3 _targetPosition;
    private bool _isMoving = false;

    // Границы экрана (вычисляются автоматически)
    private float _minX;
    private float _maxX;
    private float _fixedY; // Фиксированная позиция по Y

    void Start()
    {
        // Вычисляем границы экрана в мировых координатах
        Camera cam = Camera.main;

        // Получаем размеры экрана в мировых единицах
        float screenHeight = cam.orthographicSize * 2f;
        float screenWidth = screenHeight * cam.aspect;

        // Вычисляем границы только по X (по Y не ограничиваем, так как он фиксирован)
        _minX = -screenWidth / 2f;
        _maxX = screenWidth / 2f;

        // Запоминаем фиксированную позицию по Y
        if (_spawnAtStart)
        {
            // Ограничиваем точку спавна по X, Y берём из _spawnPosition
            float clampedSpawnX = Mathf.Clamp(_spawnPosition.x, _minX, _maxX);
            _fixedY = _spawnPosition.y;

            _targetPosition = new Vector3(clampedSpawnX, _fixedY, 0);
            transform.position = _targetPosition;
        }
        else
        {
            // Используем текущую позицию в сцене
            _fixedY = transform.position.y;
            _targetPosition = transform.position;
        }
    }

    void Update()
    {
        // Получаем позицию касания (палец или мышь)
        Vector3 screenPos = Vector3.zero;
        bool isTouching = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            screenPos = Camera.main.ScreenToWorldPoint(touch.position);
            screenPos.z = 0;
            isTouching = true;
        }
        else if (Input.GetMouseButton(0))
        {
            screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            screenPos.z = 0;
            isTouching = true;
        }

        if (isTouching)
        {
            // Ограничиваем позицию только по X
            float clampedX = Mathf.Clamp(screenPos.x, _minX, _maxX);

            // Y остаётся фиксированным
            _targetPosition = new Vector3(clampedX, _fixedY, 0);
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }

        // Плавно двигаем корабль только по X
        if (_isMoving)
        {
            Vector3 currentPos = transform.position;
            currentPos.x = Mathf.Lerp(currentPos.x, _targetPosition.x, _smoothSpeed * Time.deltaTime);
            // Y не меняется
            transform.position = currentPos;
        }
    }

    // Публичный метод для телепортации в точку спавна
    public void TeleportToSpawn()
    {
        float clampedSpawnX = Mathf.Clamp(_spawnPosition.x, _minX, _maxX);
        _fixedY = _spawnPosition.y;

        _targetPosition = new Vector3(clampedSpawnX, _fixedY, 0);
        transform.position = _targetPosition;
    }

    // Публичный метод для установки новой точки спавна
    public void SetSpawnPosition(Vector2 newSpawn)
    {
        _spawnPosition = newSpawn;
    }

    // Публичный метод для получения текущей точки спавна
    public Vector2 GetSpawnPosition()
    {
        return _spawnPosition;
    }
}
