using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private float money;
    [SerializeField] private float startMoney = 150f;
    [SerializeField] private float moneyMultiplyIndex = 0.3f;
    [SerializeField] private float minRewardForEnemyKill = 5f;

    private void Start()
    {
        money = startMoney;
        EnemyUnit.PayEnemyReward += AddEnemyReward; 
    }
    private void Update()
    {
        money += Time.deltaTime * moneyMultiplyIndex;
    }

    public bool HasEnoughMoney(float moneyToCheck)
    {
        if (moneyToCheck <= money)
        {
            return true;
        }
        else
        {
            Debug.Log("Player hasn't so much money!");
            return false;
        }
    }

    public void DeductMoney(float moneyToDeduct)
    {
        if (HasEnoughMoney(moneyToDeduct))
        {
            money -= moneyToDeduct;
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
            money += moneyToAdd;
        }
        else
        {
            Debug.LogError("You have tried to add negative amount of money or zero!");
        }

    }

    private void AddEnemyReward()
    {
        AddMoney(Random.Range(minRewardForEnemyKill, minRewardForEnemyKill * 1.5f));
    }

    public int GetCurrentMoney()
    {
        return Mathf.RoundToInt(money);
    }
}
