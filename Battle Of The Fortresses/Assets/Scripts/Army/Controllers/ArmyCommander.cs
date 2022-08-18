using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class ArmyCommander : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private ArmyFormator armyFormator;
    [SerializeField] private ArmyStateEffectController armyStateEffectController;


    [SerializeField] private StatesOfArmy currentStateOfArmy = StatesOfArmy.Follow;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Rigidbody rigidbody;

    [SerializeField] private float lineUpSpeed = 1f;

    [SerializeField] private float armyObserverRadius = 5f;
    [SerializeField] private float attackRadiusCounter;
    private float checkAttackRadiusTime = 1f;
    

    private readonly List<ArmyUnit> UnitsInArmy = new List<ArmyUnit>();
    private readonly List<ArmyUnit> UnitsWithoutTarget = new List<ArmyUnit>();
    private readonly List<ArmyUnit> UnitsToFollow = new List<ArmyUnit>();
    private void Start()
    {
        attackRadiusCounter = 0;
        GetComponent<SphereCollider>().radius = armyObserverRadius;
    }
    private void Update()
    {
        Debug.Log($"UnInArmy: {UnitsInArmy.Count} \t UnWithOutT: {UnitsWithoutTarget.Count} \t UnToF: {UnitsToFollow.Count}");

        if (currentStateOfArmy == StatesOfArmy.StandStill)
        {
            return;
        }

        if (UnitsToFollow.Count >= 1)
        {
            armyFormator.SetFormation(UnitsToFollow, transform, lineUpSpeed);
        }

        if (currentStateOfArmy == StatesOfArmy.BeReadyToFight)
        {
            if (attackRadiusCounter <= 0)
            {

                if (UnitsInArmy.Count != 0)
                {
                    Collider[] enemiesInArmyObserver = Physics.OverlapSphere(transform.position, armyObserverRadius, enemyLayer);

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

    private void FixedUpdate()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0 && currentStateOfArmy != StatesOfArmy.StandStill)
        {
            rigidbody.velocity = new Vector3(joystick.Horizontal * lineUpSpeed, 0, joystick.Vertical * lineUpSpeed); // Move rb by joystick input
            foreach (var unit in UnitsToFollow)
            {
                unit.RotateUnitToMoveDirection(rigidbody.velocity);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ArmyUnit>() && UnitsInArmy.Contains(other.GetComponent<ArmyUnit>()) == false)
        {
            AddUnitToArmyList(other.GetComponent<ArmyUnit>());
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
                currentUnit.ChangeCurrentState(StatesOfArmy.BeReadyToFight);
            }

            armyStateEffectController.ChangeEffectColor(Color.red);
        }
        else if (commandNumber == (int)StatesOfArmy.Follow)
        {
            armyStateEffectController.ChangeEffectColor(Color.blue);
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



    public void AddUnitToArmyList(ArmyUnit unit)
    {
        UnitsInArmy.Add(unit);
        UnitsWithoutTarget.Add(unit);
        UnitsToFollow.Add(unit);
    }

    public void KillUnit(ArmyUnit unit)
    {
        UnitsInArmy.Remove(unit);
        UnitsWithoutTarget.Remove(unit);
        UnitsToFollow.Remove(unit);
    }

    

    private void OnDrawGizmosSelected()
    {
         Gizmos.DrawWireSphere(transform.position, armyObserverRadius);
    }
}

public enum StatesOfArmy
{
    BeReadyToFight,
    StandStill,
    Follow
}


