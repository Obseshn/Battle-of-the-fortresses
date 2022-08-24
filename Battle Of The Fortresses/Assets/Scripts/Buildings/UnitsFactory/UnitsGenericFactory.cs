using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsGenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    public T GetNewInstance(Transform spawnPosition)
    {
        return Instantiate(_prefab, spawnPosition.position, Quaternion.identity);
    }

    /*public T GetNewInstance()
    {
        return Instantiate(_prefab, defaultSpawnPos.position, Quaternion.identity);
    }*/
}
