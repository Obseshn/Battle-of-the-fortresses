using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : ArmyUnit
{
    private static float MaxHealth = 25f;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    protected override void DoAttack(Transform targetPosition)
    {
        base.DoAttack(targetPosition);
        Debug.Log("Wizard attacked!!");
    }
}
