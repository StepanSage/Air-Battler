using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnableFactory _spawnableFactory;
    [SerializeField][Range (0, 10)] private float _spawnInterval;
    [SerializeField] private bool _isStart = true;
     

    private void Start()
    {
        //_spawnableFactory = gameObject.GetComponent<SpawnableFactory>();
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            if (_isStart == true) _spawnableFactory.Spawn();

            yield return new WaitForSeconds(_spawnInterval);

            if (_isStart == false) _spawnableFactory.Spawn();
        }

        
    }

    
}
