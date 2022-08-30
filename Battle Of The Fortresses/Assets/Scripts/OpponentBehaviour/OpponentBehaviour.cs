using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentBehaviour : MonoBehaviour
{
    private OpponentTimingGenerator opponentTimingGenerator;

    private int countForCreeping = 5;
    private int countOfUnitInArmy;

    [SerializeField] private Barrack opponentBarrack;
    [SerializeField] private ArmyCommander opponnentArmyCommander;



    private void Start()
    {
        opponentTimingGenerator.timeToSpawnUnit += opponentBarrack.CreateRandomUnit;
    }

    private void OnDisable()
    {
        opponentTimingGenerator.timeToSpawnUnit -= opponentBarrack.CreateRandomUnit;
    }

    private void UpdateCountOfUnit()
    {
        countOfUnitInArmy = opponnentArmyCommander.GetCountOfArmy();
        if (countOfUnitInArmy >= countForCreeping && opponnentArmyCommander.GetStateOfArmy() != StatesOfArmy.BeReadyToFight)
        {

        }
    }
}
