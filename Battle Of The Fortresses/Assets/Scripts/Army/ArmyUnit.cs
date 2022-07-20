using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmyUnit : MonoBehaviour
{
    protected abstract float Health { get; set; }
    protected abstract float AttackDamage { get; }

    protected abstract void DoAttack(float attackDamage);
    protected abstract void TakeDamage(float damageToTake);

    protected abstract void DestroyYourself();
}
