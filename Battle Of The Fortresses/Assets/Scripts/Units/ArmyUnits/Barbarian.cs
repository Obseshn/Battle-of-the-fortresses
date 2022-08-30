using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : ArmyUnit
{
    private static float MaxHealth = 1000f;
    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

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
        base.DoAttack(targetPosition);
        Collider[] enemiesToAttack = Physics.OverlapSphere(transform.position, attackController.attackRadius, enemyLayer);
        foreach (var enemy in enemiesToAttack)
        {
            enemy.GetComponent<IDamageAble>().TakeDamage(AttackDamage);
        }
        Debug.Log("Barbarian has attacked: " + targetPosition.name);
    }
}
