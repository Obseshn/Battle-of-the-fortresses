using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ArmyUnit
{
    protected override float Health { get => _health; set => _health = value; }
    protected override float AttackDamage => _attackDamage;

    private float _health = 5;
    private float _attackDamage = 2f;

    protected override void DoAttack(float attackDamage)
    {
        Debug.Log("Knight has attacked!");
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
