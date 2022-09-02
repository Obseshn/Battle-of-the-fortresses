using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ArmyUnit
{
    private static float MaxHealth = 25f;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void DestroyYourself()
    {
       
    }

    protected override void DoAttack(Transform targetPosition)
    {
        base.DoAttack(targetPosition);
        Debug.Log("Knight attacked!!");
    }

}
