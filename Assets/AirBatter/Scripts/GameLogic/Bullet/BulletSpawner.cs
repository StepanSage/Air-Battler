using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _spawnPoin;
    [Range(0.5f, 3f)] [SerializeField] private float _spawnInterval;

    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    private void Spawn()
    {
        Instantiate(_bullet, _spawnPoin.position, _spawnPoin.rotation);
    }

    private IEnumerator StartSpawn()
    {
        while(true)
        {
            Spawn();
            yield return new WaitForSeconds(_spawnInterval);
            Debug.Log("buller is spawn");
        }
    }
}
