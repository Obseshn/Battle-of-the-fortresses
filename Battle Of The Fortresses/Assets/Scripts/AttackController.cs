using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackController : MonoBehaviour
{
    [SerializeField] private string _tag_Target;
    [SerializeField] private Transform _attackTarget;
 
    [SerializeField] private float _attackCooldown = 2;
    [SerializeField] private float _attackCDCounter;
    [SerializeField] private float _attackDamage = 1f;

    public event Action<Transform> TargetEnteredToAttackRadius;
    public event Action CurrentAttackTargetHasLosted;

    private void Update()
    {
        if (_attackTarget != null && _attackCDCounter <= 0)
        {
            DoAttack(_attackTarget);
        }
        if (_attackCDCounter >= 0)
        {
            _attackCDCounter -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag_Target))
        {
            _attackTarget = other.transform;
            TargetEnteredToAttackRadius?.Invoke(other.transform);
            _attackCDCounter = _attackCooldown;
            Debug.Log("Target has entered to the attack radius");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tag_Target))
        {
            _attackTarget = null;
            CurrentAttackTargetHasLosted?.Invoke();
            Debug.Log("Target has left from the attack radius");
        }
    }

    private void DoAttack(Transform target)
    {
        Debug.Log(target.name + "has been attacked!");
        target.GetComponent<HealthSystem>().TakeDamage(_attackDamage);
        _attackCDCounter = _attackCooldown;
    }
}
