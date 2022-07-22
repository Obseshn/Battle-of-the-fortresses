using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ArmyUnit
{
    protected override float Health { get => _health; set => _health = value; }
    protected override float AttackDamage => _attackDamage;
    protected override Transform AttackTarget { get => _attackTarget; set => _attackTarget = value; }

    [SerializeField] private Transform _attackTarget;

    private float _health = 5;
    private float _attackDamage = 2f;

    protected override void DoAttack(float attackDamage, Transform targetPosition)
    {
        Debug.Log("Knight has attacked: " + targetPosition.name);
    }

    protected override void TakeDamage(float damageToTake)
    {
        if (damageToTake > Health)
        {
            DestroyYourself();
        }
        Health -= damageToTake;
    }

    protected override void DestroyYourself()
    {
        // Отписаться от всех эвентов
        Destroy(gameObject);
    }
}
