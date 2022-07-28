using UnityEngine;

public class Player : ArmyUnit
{
    protected override float AttackDamage => throw new System.NotImplementedException();

    protected override void DoAttack(float attackDamage, Transform targetPosition)
    {
        Debug.Log("Player has attacked!");
    }

}
