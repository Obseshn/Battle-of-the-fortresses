using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ArmyUnit
{
    protected override float AttackDamage => throw new System.NotImplementedException();

    protected override void DoAttack(float attackDamage, Transform targetPosition)
    {
        Debug.Log("Knight has attacked: " + targetPosition.name);
    }

}
