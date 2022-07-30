using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingSystem : MonoBehaviour
{
    [Header("Settings: ")]
    [SerializeField] private bool isUnitShooter;
    [SerializeField] private string tagOfHostileEntity;

    [Header("Controllers: ")]
    [SerializeField] private ChaseController chaseController;
    [SerializeField] private ViewingController viewingController;
    [SerializeField] private AttackController attackController;
    private void Start()
    {
        viewingController.tagOfTarget = tagOfHostileEntity;
        attackController.tagOfTarget = tagOfHostileEntity;

        viewingController.FindedViewTarget += OnViewingControllerFindedTarget;
        viewingController.LostViewTarget += OnViewingControllerLostTarget;

        
        attackController.FindedTargetEvent += OnAttackControllerFindTarget;
        attackController.LostTargetEvent += OnAttackControllerLostTarget;
    }

    private void OnViewingControllerFindedTarget(Transform target)
    {
        chaseController.SetChaseTarget(target);
    }

    private void OnViewingControllerLostTarget()
    {
        chaseController.RemoveChaseTarget();
    }

    private void OnAttackControllerFindTarget(Transform target)
    {
        viewingController.SetViewingTarget(target);
        chaseController.RemoveChaseTarget();
    }

    private void OnAttackControllerLostTarget(Transform target)
    {
        chaseController.SetChaseTarget(target);
    }
}
