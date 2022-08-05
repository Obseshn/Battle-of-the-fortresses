using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsGenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] public Transform _spawnPoint;

    public T GetNewInstance()
    {
        return Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
    }
}
