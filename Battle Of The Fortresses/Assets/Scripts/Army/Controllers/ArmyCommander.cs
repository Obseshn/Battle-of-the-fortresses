using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ArmyCommander : MonoBehaviour
{
    [SerializeField] private float lineUpSpeed = 1f;
    [SerializeField] private Barrack barrak;
    [SerializeField] private ArmyFormator armyFormator;
    [SerializeField] private ArmyStateEffectController armyStateEffectController;
    [SerializeField] private ArmyCommands currentStateOfArmy = ArmyCommands.Follow;
    [SerializeField] private Transform armyAttackPos;
    [SerializeField] private Transform armyFollowPos;
    [SerializeField] private LayerMask layerMask;
    private Transform currentArmyPos;

    [SerializeField] private float armyObserverRadius = 5f;
    private float checkAttackRadiusTime = 1f;
    private float attackRadiusCounter;

    private readonly List<GameObject> UnitsInArmy = new List<GameObject>();
    private void Start()
    {
        currentArmyPos = armyFollowPos;
        barrak.UnitCreatedEvent += AddUnitToArmyList;
        attackRadiusCounter = 0;
    }
    private void Update()
    {
        if (currentStateOfArmy == ArmyCommands.StandStill)
        {
            return;
        }
        if (UnitsInArmy.Count >= 1)
        {
            armyFormator.SetFormation(UnitsInArmy, currentArmyPos, lineUpSpeed);
        }

        if (currentStateOfArmy == ArmyCommands.BeReadyToFight)
        {
            if (attackRadiusCounter <= 0)
            {
                /*Collider[] enemiesInArmyObserver = Physics.OverlapSphere(currentArmyPos.position, armyObserverRadius, layerMask);
                if (enemiesInArmyObserver.Length != 0)
                {
                    foreach (var unit in UnitsInArmy)
                    {
                        unit.GetComponentInChildren<ViewingController>().SetViewingTarget(enemiesInArmyObserver[Random.Range(0, enemiesInArmyObserver.Length)].transform); // After this units still have target!
                    }
                    attackRadiusCounter = checkAttackRadiusTime;
                }*/
                
            }
            else
            {
                attackRadiusCounter -= Time.deltaTime;
            }
            
        }
    }

    public void ChangeCurrentStateOfArmy(int commandNumber)
    {
        currentStateOfArmy = (ArmyCommands)commandNumber;
        if (commandNumber == (int)ArmyCommands.BeReadyToFight)
        {
            armyStateEffectController.ChangeEffectColor(Color.red);
            currentArmyPos = armyAttackPos;
        }
        else if (commandNumber == (int)ArmyCommands.Follow)
        {
            armyStateEffectController.ChangeEffectColor(Color.blue);
            currentArmyPos = armyFollowPos;
        }
        else if (commandNumber == (int)ArmyCommands.StandStill)
        {
            armyStateEffectController.ChangeEffectColor(Color.green);
        }
    }

    public void AddUnitToArmyList(GameObject unit)
    {
        UnitsInArmy.Add(unit);
    }

    public void KillUnit(GameObject unit)
    {
        UnitsInArmy.Remove(unit);
    }

    public enum ArmyCommands
    {
        BeReadyToFight,
        StandStill,
        Follow
    }

    private void OnDrawGizmosSelected()
    {
        if (currentArmyPos != null)
        {
            Gizmos.DrawWireSphere(currentArmyPos.position, armyObserverRadius);
        }
        
    }
}


