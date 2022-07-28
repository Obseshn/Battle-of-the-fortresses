using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmyUnit : MonoBehaviour
{
    protected abstract float AttackDamage { get; }
    protected abstract void DoAttack(float attackDamage, Transform targetPosition);
}
