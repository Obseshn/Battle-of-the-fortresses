using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmyUnit : MonoBehaviour
{
    protected abstract float Health { get; set; }
    protected abstract float AttackDamage { get; }
    protected abstract Transform AttackTarget { get; set; }
    protected abstract void DoAttack(float attackDamage, Transform targetPosition);
    protected abstract void TakeDamage(float damageToTake);
    protected abstract void DestroyYourself();
}
