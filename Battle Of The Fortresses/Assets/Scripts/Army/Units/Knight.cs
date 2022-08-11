using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ArmyUnit
{
    private static float MaxHealth = 10000f;

    private void Awake()
    {
        Armor = 10f;
        CurrentHealth = MaxHealth;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void DestroyYourself()
    {
        Debug.Log(gameObject.name + "has been destroyed");
        Destroy(gameObject);
    }

    protected override void DoAttack(Transform targetPosition)
    {
        Debug.Log("Knight attacked!!");
        targetPosition.GetComponent<IDamageAble>().TakeDamage(AttackDamage);
    }

}
