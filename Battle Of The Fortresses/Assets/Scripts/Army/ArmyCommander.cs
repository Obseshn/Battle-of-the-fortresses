using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ArmyCommander : MonoBehaviour
{
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private float _unitSpeed;
    [SerializeField] private Barrack _barrak;
    [SerializeField] private ArmyFormator armyFormator /*= new ArmyFormator()*/;
    [SerializeField] private ArmyStateEffectController armyStateEffectController;
    [SerializeField] private ArmyCommands currentStateOfArmy = ArmyCommands.Follow;

    private readonly List<GameObject> _spawnedUnits = new List<GameObject>();
    private void Start()
    {
        _barrak.UnitCreatedEvent += AddUnitToArmyList;
    }
    private void Update()
    {
        // ≈сли игрок двигаетс€, то арми€ должна идти за ним.
        if (currentStateOfArmy == ArmyCommands.Follow)
        {
            armyFormator.SetFormation(_spawnedUnits, transform, _unitSpeed);
        }
        else if (currentStateOfArmy == ArmyCommands.BeReadyToFight)
        {

        }
        else if (currentStateOfArmy == ArmyCommands.StandStill)
        {

        }
    }

    private void ChangeCurrentStateOfArmy(ArmyCommands command)
    {
        currentStateOfArmy = command;
        if (command == ArmyCommands.BeReadyToFight)
        {
            armyStateEffectController.ChangeEffectColor(Color.red);
        }
        else if (command == ArmyCommands.Follow)
        {
            armyStateEffectController.ChangeEffectColor(Color.blue);
        }
        else if (command == ArmyCommands.StandStill)
        {
            armyStateEffectController.ChangeEffectColor(Color.green);
        }
    }

    public void AddUnitToArmyList(GameObject unit)
    {
        unit.GetComponent<HealthSystem>().ObjectHasBeenDestroyedEvent += KillUnit;
        _spawnedUnits.Add(unit);
    }

    public void KillUnit(GameObject unit)
    {
        unit.GetComponent<HealthSystem>().ObjectHasBeenDestroyedEvent -= KillUnit;
        _spawnedUnits.Remove(unit);
    }

    private enum ArmyCommands
    {
        BeReadyToFight,
        StandStill,
        Follow
    }

}


