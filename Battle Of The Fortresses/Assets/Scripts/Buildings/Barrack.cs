using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Barrack : MonoBehaviour
{
    [SerializeField] private KnightFactory knightFactory;
    [SerializeField] private ArcherFactory archerFactory;
    [SerializeField] private GnomeFactory gnomeFactory;

    private readonly float _knightCreatingDelay = 3f;

    public event Action<GameObject> UnitCreatedEvent;

    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i % 2 == 0)
            {
                CreateKnightWithDelay(i);
            }
            if (i % 3 == 0)
            {
                CreateGnomeWithDelay(i);
            }
            else
            {
                CreateArcherWithDelay(i);
            }
        }
    }

    public void CreateKnightWithDelay(float delayTime)
    {
        StartCoroutine(CreateKnightDelay(delayTime));
    }

    public void CreateArcherWithDelay(float delayTime)
    {
        StartCoroutine(CreateArcherDelay(delayTime));
    }
    IEnumerator CreateArcherDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        var prefab = archerFactory.GetNewInstance();
        UnitCreatedEvent.Invoke(prefab.gameObject);
    }
    
    IEnumerator CreateKnightDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        var prefab = knightFactory.GetNewInstance();
        UnitCreatedEvent?.Invoke(prefab.gameObject);
    }

    public void CreateGnomeWithDelay(float delayInSeconds)
    {
        StartCoroutine(CreateGnomeDelay(delayInSeconds));
    }

    IEnumerator CreateGnomeDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        var prefab = gnomeFactory.GetNewInstance();
        UnitCreatedEvent?.Invoke(prefab.gameObject);
    }
}
