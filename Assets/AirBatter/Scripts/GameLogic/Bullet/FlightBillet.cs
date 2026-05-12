using UnityEngine;

public class FlihgtBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed  = 5;

    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        transform.Translate(transform.up * _bulletSpeed * Time.deltaTime);
    }

}
