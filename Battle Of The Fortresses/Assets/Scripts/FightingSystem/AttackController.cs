using UnityEngine;
using System;

[RequireComponent(typeof(SphereCollider))]
public class AttackController : MonoBehaviour
{
    [SerializeField] public string tagOfTarget;
    [SerializeField] private Transform attackTarget;
 
    [SerializeField] private readonly float attackCooldown = 2;
    [SerializeField] private float attackCDCounter;
    [SerializeField] public float attackRadius;

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
            attackTarget = other.transform;
            FindedTargetEvent?.Invoke(other.transform);
            attackCDCounter = attackCooldown;
            Debug.Log("Target has entered to the attack radius");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagOfTarget))
        {
            attackTarget = null;
            LostTargetEvent?.Invoke(other.transform);
            Debug.Log("Target has left from the attack radius");
        }
    }

    // Ready to attack event
    private void SendReadyToAttack(Transform target)
    {
        Debug.Log(target.name + " has been attacked!");
        ReadyToAttackEvent?.Invoke(target);
        attackCDCounter = attackCooldown;
    }
}
