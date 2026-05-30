using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject<T> : IPoolObject<T> where T : class
{
    private readonly Queue<T> _pool = new Queue<T>();
    private readonly Func<T> _creatFunc;

    public PoolObject(Func<T> creatFunc)
    {
        _creatFunc = creatFunc ?? throw new ArgumentNullException(nameof(_creatFunc)); ;
    }

    public T Get()
    {
        T obj;

        if (_pool.Count > 0)
            obj = _pool.Dequeue();
        else
            obj = _creatFunc();

        return obj;
    }

    public void Return(T obj)
    {

        if (obj == null)
        {
            Debug.LogError("Attempted to return null object to pool");
            return;
        }
           
       _pool.Enqueue(obj);
    }

    public int Size() => _pool.Count;

    public void Clean() => _pool.Clear();

    public void Prefarm(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = _creatFunc();
            _pool.Enqueue(obj);
        }
    }
}
