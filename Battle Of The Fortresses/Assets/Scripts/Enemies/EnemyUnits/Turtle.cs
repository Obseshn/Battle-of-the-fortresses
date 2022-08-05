using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Enemy
{
    public override void TakeDamage(float damage)
    {
        if (CurrentHealth <= damage)
        {
            DestroyYourself();
            return;
        }

        CurrentHealth -= (damage - Armor);
    }

    protected override void DestroyYourself()
    {
        Destroy(gameObject);
    }

    protected override void DoAttack(Transform targetPosition)
    {
        Debug.Log("Turtle attacked!");
    }
}
