using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ArmyCommander : MonoBehaviour
{
    [SerializeField] private float lineUpSpeed = 1f;
    [SerializeField] private Barrack barrak;
    [SerializeField] private ArmyFormator armyFormator;
    [SerializeField] private ArmyStateEffectController armyStateEffectController;
    [SerializeField] private StatesOfArmy currentStateOfArmy = StatesOfArmy.Follow;
    [SerializeField] private Transform armyAttackPos;
    [SerializeField] private Transform armyFollowPos;
    [SerializeField] private LayerMask layerMask;
    private Transform currentArmyPos;

    [SerializeField] private float armyObserverRadius = 5f;
    private float checkAttackRadiusTime = 1f;
    [SerializeField] private float attackRadiusCounter;

    private readonly List<GameObject> UnitsInArmy = new List<GameObject>();
    private readonly List<GameObject> UnitsWithoutTarget = new List<GameObject>();
    private readonly List<GameObject> UnitsToFollow = new List<GameObject>();
    private void Start()
    {
        currentArmyPos = armyFollowPos;
        barrak.UnitCreatedEvent += AddUnitToArmyList;
        attackRadiusCounter = 0;
    }
    private void Update()
    {
        Debug.Log($"UnInArmy: {UnitsInArmy.Count} \t UnWithOutT: {UnitsWithoutTarget.Count} \t UnToF: {UnitsToFollow.Count}");

        if (currentStateOfArmy == StatesOfArmy.StandStill)
        {
            return;
        }

        if (UnitsInArmy.Count >= 1)
        {
            armyFormator.SetFormation(UnitsToFollow, currentArmyPos, lineUpSpeed);
        }

        if (currentStateOfArmy == StatesOfArmy.BeReadyToFight)
        {
            if (attackRadiusCounter <= 0)
            {

                if (UnitsInArmy.Count != 0)
                {
                    Collider[] enemiesInArmyObserver = Physics.OverlapSphere(currentArmyPos.position, armyObserverRadius, layerMask);

                    FindUnitsWithoutTarget();

                    if (enemiesInArmyObserver.Length != 0 && UnitsWithoutTarget.Count > 0)
                    {
                        GiveTargetToUnits(enemiesInArmyObserver);
                    }
                    attackRadiusCounter = checkAttackRadiusTime;
                }
            }
            else
            {
                attackRadiusCounter -= Time.deltaTime;
            }

        }
    }

    private void FindUnitsWithoutTarget()
    {
        foreach (var unit in UnitsInArmy)
        {
            if (unit.GetComponent<ArmyUnit>().ViewingTarget == null && UnitsWithoutTarget.Contains(unit) == false)
            {
                UnitsWithoutTarget.Add(unit);
                UnitsToFollow.Add(unit);
            }
        }
    }
    private void GiveTargetToUnits(Collider[] enemiesInArmyObserver)
    {
        foreach (var unit in UnitsWithoutTarget)
        {
            unit.GetComponent<ArmyUnit>().SetViewingTarget(enemiesInArmyObserver[Random.Range(0, enemiesInArmyObserver.Length)].transform); // !!!!!!
            UnitsToFollow.Remove(unit);
        }
        UnitsWithoutTarget.Clear();
        Debug.Log(UnitsWithoutTarget.Capacity + " " + UnitsWithoutTarget.Count);
    }

    public void ChangeCurrentStateOfArmy(int commandNumber)
    {
        currentStateOfArmy = (StatesOfArmy)commandNumber;
        if (commandNumber == (int)StatesOfArmy.BeReadyToFight)
        {
            foreach (var unit in UnitsInArmy)
            {
                ArmyUnit currentUnit = unit.GetComponent<ArmyUnit>();

                currentUnit.RemoveViewingTarget();
                /*currentUnit.FindTargetEvent += OnArmyUnitFindTarget;    // EVENT SUBSCRIPTION*/
                currentUnit.ChangeCurrentState(StatesOfArmy.BeReadyToFight);
            }

            armyStateEffectController.ChangeEffectColor(Color.red);
            currentArmyPos = armyAttackPos;
        }
        else if (commandNumber == (int)StatesOfArmy.Follow)
        {
            armyStateEffectController.ChangeEffectColor(Color.blue);
            currentArmyPos = armyFollowPos;
            foreach(var unit in UnitsInArmy)
            {
                unit.GetComponent<ArmyUnit>().ChangeCurrentState(StatesOfArmy.Follow);
            }
        }
        else if (commandNumber == (int)StatesOfArmy.StandStill)
        {
            armyStateEffectController.ChangeEffectColor(Color.green);
            foreach (var unit in UnitsInArmy)
            {
                unit.GetComponent<ArmyUnit>().ChangeCurrentState(StatesOfArmy.StandStill);
            }
        }
    }

    public void AddUnitToArmyList(GameObject unit)
    {
        UnitsInArmy.Add(unit);
        UnitsWithoutTarget.Add(unit);
        UnitsToFollow.Add(unit);
    }

    public void KillUnit(GameObject unit)
    {
        /*unit.GetComponent<ArmyUnit>().FindTargetEvent -= OnArmyUnitFindTarget;*/
/*        unit.GetComponent<ArmyUnit>().LostTargetEvent -= OnArmyUnitFindTarget;*/
        UnitsInArmy.Remove(unit);
        UnitsWithoutTarget.Remove(unit);
        UnitsToFollow.Remove(unit);
    }

    

    private void OnDrawGizmosSelected()
    {
        if (currentArmyPos != null)
        {
            Gizmos.DrawWireSphere(currentArmyPos.position, armyObserverRadius);
        }
        
    }

    /*private void OnArmyUnitLostTarget(ArmyUnit unit)
    {
        UnitsWithoutTarget.Add(unit.gameObject);
        Debug.Log("OnArmyUnitLostTarget method");
    }*/

    /*private void OnArmyUnitFindTarget(ArmyUnit unit)
    {
        UnitsWithoutTarget.Remove(unit.gameObject);
    }*/

}

public enum StatesOfArmy
{
    BeReadyToFight,
    StandStill,
    Follow
}


