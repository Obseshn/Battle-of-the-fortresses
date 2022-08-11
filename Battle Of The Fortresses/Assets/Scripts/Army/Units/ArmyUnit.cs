using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class ArmyUnit : UnitBase
{
    [SerializeField] protected StatesOfArmy currentState;

    
    private void Start()
    {
        attackController.tagOfTarget = TagOfTarget;
    }

    private void Update()
    {
        if (currentState == StatesOfArmy.BeReadyToFight)
        {
            if (ViewingTarget == null)
                return;

            if (AttackTarget == null)
            {
                MoveTo(ViewingTarget);
            }

            transform.LookAt(ViewingTarget);
        }
         
    }

    public void ChangeCurrentState(StatesOfArmy newState)
    {
        currentState = newState;
    }
}
