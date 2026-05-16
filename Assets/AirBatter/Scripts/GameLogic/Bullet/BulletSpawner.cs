using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _spawnPoin;

    [Header("Audio")]
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioSource _audioSource;
    [Range(0.5f, 3f)] [SerializeField] private float _spawnInterval;
     
    private void Start()
    {
        StartCoroutine(StartSpawn());
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

    private void Spawn()
    {
        Instantiate(_bullet, _spawnPoin.position, _spawnPoin.rotation);
        PlayAudioShoot();
       
    }

    private void PlayAudioShoot()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.10f);
        _audioSource.PlayOneShot(_clip);
    }

}
