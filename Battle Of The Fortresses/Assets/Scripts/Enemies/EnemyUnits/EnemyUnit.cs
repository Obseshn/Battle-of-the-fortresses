using UnityEngine;
using System;

public abstract class EnemyUnit : UnitBase
{
    
    [SerializeField] public Transform spotPosition;
    [SerializeField] protected bool isOnSpotPosition;

    /*[SerializeField] private ViewingController viewingController;*/

    public Action<EnemyUnit> enemyDied;

    private void OnEnable()
    {
        /*viewingController.FindedViewTarget += SetViewingTarget;
        viewingController.LostViewTarget += RemoveViewingTarget;*/
    }
    private void Start()
    {
        attackController.tagOfTarget = TagOfTarget;
       /* viewingController.tagOfTarget = TagOfTarget;*/
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

    public void SwitchIsOnSpotPosBool(bool newState)
    {
        isOnSpotPosition = newState;
        if (newState && ViewingTarget == null)
        {
            animator.SetBool(isMovingBoolName, false);
        }
        else
        {
            animator.SetBool(isMovingBoolName, true);
        }
    }
}
