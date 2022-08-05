using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageAble
{
    public float CurrentHealth { get; set; }
    [SerializeField] protected string TagOfTarget;
    [SerializeField] protected float AttackDamage;
    [SerializeField] protected float Armor;
    [SerializeField] protected float MoveSpeed;
    [SerializeField] public Transform spotPosition;
    [SerializeField] public bool isOnSpotPosition;

    protected Transform ViewingTarget;
    protected Transform AttackTarget;

    [SerializeField] private AttackController attackController;
    [SerializeField] private ViewingController viewingController;

    private void OnEnable()
    {
        attackController.FindedTargetEvent += SetAttackTarget;
        attackController.LostTargetEvent += RemoveAttackTarget;
        attackController.ReadyToAttackEvent += DoAttack;

        viewingController.FindedViewTarget += SetViewingTarget;
        viewingController.LostViewTarget += RemoveViewingTarget;
    }
    private void Start()
    {
        attackController.tagOfTarget = TagOfTarget;
        viewingController.tagOfTarget = TagOfTarget;
    }

    private void Update()
    {
        if (ViewingTarget == null)
        {
            if (isOnSpotPosition)
            {
                return;
            }

            MoveTo(spotPosition);
            transform.LookAt(spotPosition);
            return;
        }
            
        if (AttackTarget == null)
        {
            MoveTo(ViewingTarget);
        }

        transform.LookAt(ViewingTarget);
    }

    protected void MoveTo(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
    }
    public abstract void TakeDamage(float damage);
    protected abstract void DoAttack(Transform targetPosition);
    protected abstract void DestroyYourself();

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

    protected void SetViewingTarget(Transform target)
    {
        ViewingTarget = target;
    }

    protected void RemoveViewingTarget()
    {
        ViewingTarget = null;
    }
}
