using System.Collections;
using UnityEngine;

public class SpawnMeteor : MonoBehaviour
{
    [SerializeField] private float _timeSpawnMeteor;
    [SerializeField] private GameObject _PrefabMeteor;
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {  
            yield return new WaitForSeconds(_timeSpawnMeteor);
            Instantiate(_PrefabMeteor, transform.position, transform.rotation);
        }
        
    }
}
