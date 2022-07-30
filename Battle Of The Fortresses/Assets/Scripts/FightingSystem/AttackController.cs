using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackController : MonoBehaviour
{
    [SerializeField] public string tagOfTarget;
    [SerializeField] private Transform attackTarget;
 
    [SerializeField] private readonly float attackCooldown = 2;
    [SerializeField] private float attackCDCounter;
    [SerializeField] private float attackDamage = 1f;

    public event Action<Transform> FindedTargetEvent;
    public event Action<Transform> LostTargetEvent;

    private void Update()
    {
        if (attackTarget != null && attackCDCounter <= 0)
        {
            DoAttack(attackTarget);
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
    private void DoAttack(Transform target)
    {
        Debug.Log(target.name + "has been attacked!");
        target.GetComponent<HealthSystem>().TakeDamage(attackDamage);
        attackCDCounter = attackCooldown;
    }
}
