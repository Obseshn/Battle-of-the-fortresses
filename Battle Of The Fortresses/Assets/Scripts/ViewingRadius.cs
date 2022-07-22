using UnityEngine;
using System;

public class ViewingRadius : MonoBehaviour
{
    [SerializeField] private string _tag_Target;

    [SerializeField] private GameObject currentTarget;

    public event FindTarget SoldierEnterInAttackRadius;
    public delegate void FindTarget(Transform target);

    public event Action CurrentViewTargetWentOutOfViewRange;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag_Target) && currentTarget == null)
        {
            currentTarget = other.gameObject;
            SoldierEnterInAttackRadius?.Invoke(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentTarget)
        {
            currentTarget = null;
            CurrentViewTargetWentOutOfViewRange?.Invoke();
        }
    }
}
