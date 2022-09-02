using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Barrack : MonoBehaviour
{
    [SerializeField] private KnightFactory knightFactory;
    [SerializeField] private WizardFactory wizardFactory;
    [SerializeField] private BarbarianFactory barbarianFactory;
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private Transform spawnPoint;

    private readonly float knightPrice = 20f;
    private readonly float wizardPrice = 30f;
    private readonly float barbarianPrice = 40f;

    private readonly float knightSpawnTime = 3f;
    private readonly float wizardSpawnTime = 5f;
    private readonly float barbarianSpawnTime = 10f;

    public event Action UnitCreatedEvent;

    private void Start()
    {
       
    }

    public void CreateRandomUnit()
    {
        int index = UnityEngine.Random.Range(0, 3); // 3 - current count of soldiers prefabs, or factories!
        if (index == 0)
        {
            CreateKnightWithDelay(knightSpawnTime);
        }
        else if (index == 1)
        {
            CreateWizardWithDelay(wizardSpawnTime);
        }
        else if (index == 2)
        {
            CreateBarbarianWithDelay(barbarianSpawnTime);
        }
    }
    public void CreateKnightWithDelay(float delayTime)
    {
        if (moneyManager.HasEnoughMoney(knightPrice))
        {
            moneyManager.DeductMoney(knightPrice);
            StartCoroutine(CreateKnightDelay(delayTime));
        }
    }
    
    public void CreateWizardWithDelay(float delayTime)
    {
        if (moneyManager.HasEnoughMoney(wizardPrice))
        {
            moneyManager.DeductMoney(wizardPrice);
            StartCoroutine(CreateWizardDelay(delayTime));
        }
    }

    public void CreateBarbarianWithDelay(float delayInSeconds)
    {
        if (moneyManager.HasEnoughMoney(barbarianPrice))
        {
            moneyManager.DeductMoney(barbarianPrice);
            StartCoroutine(CreateBarbarianDelay(delayInSeconds));
        }
    }


    IEnumerator CreateWizardDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        var prefab = wizardFactory.GetNewInstance(spawnPoint);
    }
    
    IEnumerator CreateKnightDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        var prefab = knightFactory.GetNewInstance(spawnPoint);
    }

    IEnumerator CreateBarbarianDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        var prefab = barbarianFactory.GetNewInstance(spawnPoint);
    }
}
