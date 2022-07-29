using UnityEngine;
using System;
using System.Collections;
public class ChaseController : MonoBehaviour
{
    [SerializeField] ViewingController viewingController;

    [SerializeField] private Transform _moveTarget;

    [SerializeField] private float _moveSpeed = 2f;

    private void Start()
    {
        viewingController.FindedViewTarget += SetChaseTarget;
        viewingController.LostViewTarget += RemoveChaseTarget;
    }

    private void Update()
    {
        if (_moveTarget != null)
        {
            MoveTo(_moveTarget.transform);
        }
    }

    private void SetChaseTarget(Transform target)
    {
        _moveTarget = target;
        Debug.Log(_moveTarget.name);
    }

    private void RemoveChaseTarget()
    {
        _moveTarget = null;
    }

    private void MoveTo(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        viewingController.FindedViewTarget -= SetChaseTarget;
        viewingController.LostViewTarget -= RemoveChaseTarget;
    }
}
