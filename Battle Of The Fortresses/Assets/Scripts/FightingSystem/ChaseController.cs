using UnityEngine;
using System;
using System.Collections;
public class ChaseController : MonoBehaviour
{
    [SerializeField] private Transform chaseTarget;

    [SerializeField] private float chaseSpeed = 2f;

    private void Update()
    {
        if (chaseTarget != null)
        {
            MoveTo(chaseTarget.transform);
        }
    }

    public void SetChaseTarget(Transform target)
    {
        chaseTarget = target;
        Debug.Log(chaseTarget.name);
    }

    public void RemoveChaseTarget()
    {
        chaseTarget = null;
    }

    private void MoveTo(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
    }
}
