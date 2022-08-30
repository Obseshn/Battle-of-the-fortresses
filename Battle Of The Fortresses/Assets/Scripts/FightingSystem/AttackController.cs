using UnityEngine;
using System;

[RequireComponent(typeof(SphereCollider))]
public class AttackController : MonoBehaviour
{
    [SerializeField] public string tagOfTarget;
    [SerializeField] public Transform attackTarget;
 
    [SerializeField] private readonly float attackCooldown = 2;
    [SerializeField] private float attackCDCounter;
    [SerializeField] public float attackRadius;
    [SerializeField] private LayerMask opponentLayer;

    public event Action<Transform> FindedTargetEvent;
    public event Action<Transform> LostTargetEvent;
    public event Action<Transform> ReadyToAttackEvent;

    private void Start()
    {
        SphereCollider attackRange = gameObject.GetComponent<SphereCollider>();
        attackRange.isTrigger = true;
        if (attackRadius <= 0)
        {
            Debug.LogError("Current attack radius less or equals to zero! Check \"AttackController\" object in children of: " + gameObject.transform.parent.name);
        }
        attackRange.radius = attackRadius;

    }
    private void Update()
    {
        if (attackTarget != null && attackCDCounter <= 0)
        {
            SendReadyToAttack(attackTarget);
        }
        if (attackCDCounter >= 0)
        {
            attackCDCounter -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagOfTarget))
        {
            SetAttackTarget(other.GetComponent<UnitBase>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == attackTarget)
        {
            RemoveAttackTarget(other.GetComponent<UnitBase>());
        }
    }

    // Ready to attack event
    private void SendReadyToAttack(Transform target)
    {
        Debug.Log(target.name + " has been attacked!");
        ReadyToAttackEvent?.Invoke(target);
        attackCDCounter = attackCooldown;
    }
    private void SetAttackTarget(UnitBase unitToAdd)
    {
        attackTarget = unitToAdd.transform;
        unitToAdd.UnitDied += RemoveAttackTarget;
        FindedTargetEvent?.Invoke(unitToAdd.transform);
        attackCDCounter = attackCooldown;
        Debug.Log("Target has entered to the attack radius");
    }
    public void RemoveAttackTarget(UnitBase unitToRemove)
    {
        unitToRemove.UnitDied -= RemoveAttackTarget;
        attackTarget = null;
        LostTargetEvent?.Invoke(unitToRemove.transform);
        
        Debug.Log("Target had been removed from attack cntrller");
        Collider[] opponentsInAttackRadius =  Physics.OverlapSphere(transform.position, attackRadius, opponentLayer);
        Debug.Log(transform.parent.name + " find: " + opponentsInAttackRadius.Length);
        if (opponentsInAttackRadius.Length > 0)
        {
            SetAttackTarget(opponentsInAttackRadius[UnityEngine.Random.Range(0, opponentsInAttackRadius.Length)].GetComponent<UnitBase>());
        }
    }
}
