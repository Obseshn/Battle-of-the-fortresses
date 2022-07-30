using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmyUnit : MonoBehaviour
{
    /*[SerializeField] private string tagOfHostileEntity;
    [SerializeField] private AttackController attackController;
    [SerializeField] private ViewingController viewingController;

    private void Start()
    {
        attackController.tagOfTarget = tagOfHostileEntity;
    }*/
    protected abstract float AttackDamage { get; }
    protected abstract void DoAttack(float attackDamage, Transform targetPosition);
}
