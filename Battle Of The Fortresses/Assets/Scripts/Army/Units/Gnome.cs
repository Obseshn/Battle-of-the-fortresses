using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : ArmyUnit
{
    [SerializeField] private LayerMask enemyLayer;
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void DestroyYourself()
    {
        Destroy(gameObject);
    }

    protected override void DoAttack(Transform targetPosition)
    {
        Collider[] enemiesToAttack = Physics.OverlapSphere(transform.position, attackController.attackRadius, enemyLayer);
        foreach (var enemy in enemiesToAttack)
        {
            enemy.GetComponent<IDamageAble>().TakeDamage(AttackDamage);
        }
        Debug.Log("Gnome has attacked: " + targetPosition.name);
    }
}
