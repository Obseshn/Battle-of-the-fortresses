using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Enemy
{
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

    }

    protected override void DestroyYourself()
    {
        Destroy(gameObject);
    }

    protected override void DoAttack(Transform target)
    {
        target.GetComponent<IDamageAble>().TakeDamage(AttackDamage);
        Debug.Log("Turtle has attacked!");
    }
}
