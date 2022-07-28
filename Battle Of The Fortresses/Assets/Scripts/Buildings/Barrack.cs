using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Barrack : MonoBehaviour
{
    [SerializeField] private KnightFactory _knightFactory;
    private readonly float _knightCreatingDelay = 3f;
    public event Action<GameObject> UnitCreatedEvent;

    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            CreateKnightWithDelay(i);
        }
    }

    public void CreateKnightWithDelay(float delayTime)
    {
        StartCoroutine(CreateUnitDelay(delayTime));
    }
    
    IEnumerator CreateUnitDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        var prefab = _knightFactory.GetNewInstance();
        UnitCreatedEvent?.Invoke(prefab.gameObject);
    }
}
