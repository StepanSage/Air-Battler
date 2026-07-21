using System;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public abstract class SpawnableFactory : MonoBehaviour
{
    public  Action OnSpawn;

    protected abstract void OnEnable();

    public abstract GameObject Spawn();
}
