using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ArmyUnit
{
    private static float MaxHealth = 100f;

    private void Start()
    {
        Armor = 10f;
        CurrentHealth = MaxHealth;
    }

    public override void TakeDamage(float damage)
    {
        if (damage >= CurrentHealth)
        {
            DestroyYourself();
            return;
        }
        CurrentHealth -= (damage - Armor);
    }

    protected override void DestroyYourself()
    {
        Debug.Log(gameObject.name + "has been destroyed");
        Destroy(gameObject);
    }

    protected override void DoAttack(Transform targetPosition)
    {
        Debug.Log("Knight attacked!!");
    }

}
