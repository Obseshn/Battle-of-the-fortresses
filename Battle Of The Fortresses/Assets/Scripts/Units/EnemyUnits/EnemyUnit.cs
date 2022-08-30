using UnityEngine;
using System;

public abstract class EnemyUnit : UnitBase
{
    
    [SerializeField] public Transform spotPosition;
    [SerializeField] protected bool isOnSpotPosition = true;
    [SerializeField] private float minEnemyReward = 5;

    public Action<EnemyUnit> EnemyDied;
    public static Action PayEnemyReward;
    private void Start()
    {
        attackController.tagOfTarget = TagOfTarget;
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
            
        if (attackController.attackTarget == null)
        {
            MoveTo(ViewingTarget);
            transform.LookAt(ViewingTarget);
            Debug.Log("Enemy is moving!");
        }

        transform.LookAt(AttackTarget);
    }

    protected override void DestroyYourself()
    {
        EnemyDied?.Invoke(this);
        PayEnemyReward?.Invoke();
        base.DestroyYourself(); 
    }
    public void SwitchIsOnSpotPosBool(bool newState)
    {
        isOnSpotPosition = newState;
        if (newState && ViewingTarget == null)
        {
            /*  animator.SetBool(isMovingBoolName, false);*/
            transform.rotation = spotPosition.rotation;
        }
        else
        {
            /*animator.SetBool(isMovingBoolName, true);*/
        }
    }
}
