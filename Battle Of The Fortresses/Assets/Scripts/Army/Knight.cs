using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ArmyUnit
{
    protected override float AttackDamage => _attackDamage;

    private float _attackDamage = 2f;

    protected override void DoAttack(float attackDamage, Transform targetPosition)
    {
        Debug.Log("Knight has attacked: " + targetPosition.name);
    }

}
