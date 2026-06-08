using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _spawnPoint;

    [Header("Audio")]
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioSource _audioSource;
    [Range(0, 1)] [SerializeField] private float _volumeShotSFX = 0.5f;

    [Range(0.5f, 3f)] [SerializeField] private float _spawnInterval;

    private IAudioManager _audioManager;
    private IPoolObject<GameObject> _poolObject;
    private GameObject _storageBullet;
   

    private void Start()
    {
        Initialized();
        StartCoroutine(StartSpawn()); 
    }

    private void Initialized()
    {
        _audioManager = ServiceLocator.Instance.Get<IAudioManager>();
        _poolObject = new PoolObject<GameObject>(() => Spawn());
        _storageBullet = new GameObject("Storage_Bullet");
    }

    private IEnumerator StartSpawn()
    {
        while (true)
        {
            WorkPool();
            PlayAudioShot();

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void WorkPool()
    {
        GameObject bullet = _poolObject.Get().gameObject;

        if (bullet.activeInHierarchy == false)
        {
            bullet.transform.position = _spawnPoint.position;
            bullet.SetActive(true);
        }
        else
        {
            _poolObject.Prefarm(1);
        }
        _poolObject.Return(bullet);
    }

    private GameObject Spawn()
    {
        var bullet =  Instantiate(_bullet, _spawnPoint.position, _spawnPoint.rotation);
        bullet.transform.parent = _storageBullet.transform;
        return bullet;
    }

  

    private void PlayAudioShot()
    {
        _audioManager.PlaySfx(_clip, _volumeShotSFX);
        //_audioSource.pitch = Random.Range(0.9f, 1.10f);
        //_audioSource.PlayOneShot(_clip);
    }

}
