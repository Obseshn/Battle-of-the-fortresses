using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private float _money;

    private bool HasEnoughMoney(float moneyToCheck)
    {
        if (moneyToCheck >= _money)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DeductMoney(float moneyToDeduct)
    {
        if (HasEnoughMoney(moneyToDeduct))
        {
            _money -= moneyToDeduct;
        }
        else
        {
            Debug.LogError("You have tried to deduct more money than you have! Use \"HasEnoughMoney\" method to avoid it.");
        }
    }

    private void AddMoney(float moneyToAdd)
    {
        if (moneyToAdd > 0)
        {
            _money += moneyToAdd;
        }
        else
        {
            Debug.LogError("You have tried to add negative amount of money or zero!");
        }

    }
}
