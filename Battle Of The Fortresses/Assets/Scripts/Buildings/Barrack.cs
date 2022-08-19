using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Barrack : MonoBehaviour
{
    [SerializeField] private KnightFactory knightFactory;
    [SerializeField] private WizardFactory wizardFactory;
    [SerializeField] private BarbarianFactory barbarianFactory;

    public event Action UnitCreatedEvent;
    public void CreateKnightWithDelay(float delayTime)
    {
        StartCoroutine(CreateKnightDelay(delayTime));
    }
    
    public void CreateWizardWithDelay(float delayTime)
    {
        StartCoroutine(CreateWizardDelay(delayTime));
    }

    public void CreateBarbarianWithDelay(float delayInSeconds)
    {
        StartCoroutine(CreateBarbarianDelay(delayInSeconds));
    }


    IEnumerator CreateWizardDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        var prefab = wizardFactory.GetNewInstance();
        /*UnitCreatedEvent.Invoke();*/
    }
    
    IEnumerator CreateKnightDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        var prefab = knightFactory.GetNewInstance();
        /*UnitCreatedEvent?.Invoke();*/
    }

    IEnumerator CreateBarbarianDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        var prefab = barbarianFactory.GetNewInstance();
        /*UnitCreatedEvent?.Invoke();*/
    }
}
