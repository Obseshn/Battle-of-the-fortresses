using System;
using UnityEngine;


public abstract class UnitBase : MonoBehaviour, IDamageAble
{
    public float CurrentHealth { get; set; }
    [SerializeField] protected string TagOfTarget;
    [SerializeField] protected float AttackDamage;
    [SerializeField] protected float Armor;
    [SerializeField] protected float MoveSpeed;

    public Transform ViewingTarget;
    protected Transform AttackTarget;

    [SerializeField] protected AttackController attackController;

    private void OnEnable()
    {
        attackController.FindedTargetEvent += SetAttackTarget;
        attackController.LostTargetEvent += RemoveAttackTarget;
        attackController.ReadyToAttackEvent += DoAttack;
    }

    protected void MoveTo(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
    }

    public virtual void TakeDamage(float damage)
    {
        if (CurrentHealth <= damage)
        {
            DestroyYourself();
            return;
        }

        CurrentHealth -= (damage - (damage * GetArmorInPercent(Armor)));
    }
    protected void SetAttackTarget(Transform target)
    {
        AttackTarget = target;
        ViewingTarget = target;
    }

    protected void RemoveAttackTarget(Transform target)
    {
        AttackTarget = null;
        ViewingTarget = target;
    }

    public void SetViewingTarget(Transform target)
    {
        ViewingTarget = target;
    }

    public void RemoveViewingTarget()
    {
        ViewingTarget = null;
    }

    protected float GetArmorInPercent(float armor)
    {
        return armor / 100;
    }

    protected abstract void DestroyYourself();

    protected abstract void DoAttack(Transform target);
}

