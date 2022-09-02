using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private MoneyManager moneyManager;

    [SerializeField] private ArmyCommander armyCommander;
    [SerializeField] private TextMeshProUGUI countOfArmyText;

    private float upgradeTextCoolDown = 1f;
    private float upgradeTextCounter;

    private void Start()
    {
        upgradeTextCounter = upgradeTextCoolDown;
    }
    private void Update()
    {
        if (upgradeTextCounter > 0)
        {
            upgradeTextCounter -= Time.deltaTime;
        }
        else
        {
            moneyText.text = moneyManager.GetCurrentMoney().ToString();
            countOfArmyText.text = armyCommander.GetCountOfArmy().ToString();
        }
    }

}
