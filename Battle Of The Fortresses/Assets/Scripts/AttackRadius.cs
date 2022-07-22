using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackRadius : MonoBehaviour
{
    [SerializeField] private string _tag_Target;

    public event Action<Transform> TargetEnteredToAttackRadius;
    public event Action CurrentAttackTargetHasLosted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag_Target))
        {
            TargetEnteredToAttackRadius?.Invoke(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tag_Target))
        {
            CurrentAttackTargetHasLosted?.Invoke();
            Debug.Log("Target has left attack radius");
        }
    }
}
