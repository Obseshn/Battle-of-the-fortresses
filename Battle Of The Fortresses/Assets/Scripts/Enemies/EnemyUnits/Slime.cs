using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyUnit
{
    public static float MaxHealth = 20f;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }
}