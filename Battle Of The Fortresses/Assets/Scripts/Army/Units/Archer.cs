using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : ArmyUnit
{
    public override void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    protected override void DestroyYourself()
    {
        Destroy(gameObject);
    }

    protected override void DoAttack(Transform targetPosition)
    {
        Debug.Log("Archer attacked!");
    }
}
