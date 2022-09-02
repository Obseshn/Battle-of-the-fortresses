using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class UnitBase : MonoBehaviour, IDamageAble
{
    public Action<UnitBase> UnitDied;

    public float CurrentHealth { get; set; }
    [SerializeField] protected string TagOfTarget;
    [SerializeField] protected float AttackDamage;
    [SerializeField] protected float Armor;
    [SerializeField] protected float MoveSpeed;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody unitRigidbody;

    private Quaternion rotateDirection;
    private float rotationSpeed = 360;

    public Transform ViewingTarget;
    [SerializeField] public Transform AttackTarget;

    [SerializeField] protected AttackController attackController;

    // Main animations names: 
    private string attackAnimTriggerName = "DoAttack";
    protected string isMovingBoolName = "IsMoving";
    private string dieTriggerName = "Die";

    private void OnEnable()
    {
        attackController.FindedTargetEvent += SetAttackTarget;
        attackController.LostTargetEvent += RemoveAttackTarget;
        attackController.ReadyToAttackEvent += DoAttack;
        unitRigidbody = GetComponent<Rigidbody>();
    }

    protected void MoveTo(Transform targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, MoveSpeed * Time.deltaTime);
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
    public void SetAttackTarget(Transform target)
    {
        AttackTarget = target;
        ViewingTarget = target;
        animator.SetBool(isMovingBoolName, false);
        Debug.Log("Enemy get attack target!");
    }

    public void RemoveAttackTarget(Transform target)
    {
        AttackTarget = null;
        ViewingTarget = target;
        if (animator != null)
        {
            animator.SetBool(isMovingBoolName, true); // !!!
        }
        
        Debug.Log("Enemy lost attack target!");
    }

    public void SetViewingTarget(Transform target)
    {
        ViewingTarget = target;
        animator.SetBool(isMovingBoolName, true);
    }

    public void RemoveViewingTarget()
    {
        ViewingTarget = null;
        animator.SetBool(isMovingBoolName, false);
    }

    protected float GetArmorInPercent(float armor)
    {
        return armor / 100;
    }

    protected virtual void DestroyYourself()
    {
        UnitDied?.Invoke(this);
        StartCoroutine(DieRoutine(1f));
    }

    IEnumerator DieRoutine(float durationInSec)
    {
        attackController.FindedTargetEvent -= SetAttackTarget;
        attackController.LostTargetEvent -= RemoveAttackTarget;
        attackController.ReadyToAttackEvent -= DoAttack;
        Destroy(attackController);
        animator.SetTrigger(dieTriggerName);
        yield return new WaitForSeconds(durationInSec);
        Debug.Log(gameObject.name + "has been destroyed");
        Destroy(gameObject);
    }

    protected virtual void DoAttack(Transform target)
    {
        if (attackController != null)
        {
            target.GetComponent<IDamageAble>().TakeDamage(AttackDamage);
            animator.SetTrigger(attackAnimTriggerName);
        }
       
    }

    public void RotateUnitToMoveDirection(Vector3 lookDir)
    {
        rotateDirection = Quaternion.LookRotation(lookDir); // Calculate rotation

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, rotationSpeed); // Apply rotation to rb
    }
}

