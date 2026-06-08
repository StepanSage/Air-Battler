using System;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public abstract class SpawnableFactory : MonoBehaviour
{
    public  Action OnSpawn;

    protected abstract void Start();

    public abstract GameObject Spawn();
}
