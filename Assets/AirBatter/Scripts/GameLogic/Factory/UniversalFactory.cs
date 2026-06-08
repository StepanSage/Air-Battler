using UnityEngine;

public class UniversalFactory : SpawnableFactory
{

    [SerializeField] private GameObject _object;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private string _nameStorage  = "Storage";

    private IPoolObject<GameObject> _poolObject;
    private GameObject _storage;
    

    protected override void Start()
    {
        if(_object!= null && _spawnPoint != null)
            Initialized();
    }

    private void Initialized()
    {
        _poolObject = new PoolObject<GameObject>(() => CreatObject());
        _storage = new GameObject(_nameStorage);
    }

    public override GameObject Spawn()
    {
        WorkPool();
        return _object;
    }

    private GameObject CreatObject()
    {
        var Product = Instantiate(_object, _spawnPoint.position, _object.transform.rotation);
        Product.transform.parent = _storage.transform;
        return Product;
    }

    private void WorkPool()
    {
        if(_poolObject.Size() == 0)
        {
            _poolObject.Prefarm(1);
            return;
        }


        GameObject objectFromPool = _poolObject.Get().gameObject;

        if (objectFromPool.activeInHierarchy == false)
        {
            objectFromPool.transform.position = _spawnPoint.position;
            objectFromPool.SetActive(true);
        }
        else
        {
            _poolObject.Prefarm(1);
        }

        _poolObject.Return(objectFromPool);

    }


}
