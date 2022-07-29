using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ViewingController : MonoBehaviour
{
    [SerializeField] private string _tagOfTarget;
    [SerializeField] private AttackController attackController;
    [SerializeField] private Transform _viewingTarget;

    private readonly float minimumViewDurationInSeconds = 2f;

    public event Action<Transform> FindedViewTarget;
    public event Action LostViewTarget;

    private void Start()
    {
        attackController.TargetEnteredToAttackRadius += SetViewingTarget;
        attackController.CurrentAttackTargetHasLosted += RemoveViewingTarget;
    }

    private void Update()
    {
        if (_viewingTarget != null)
        {
            transform.parent.LookAt(_viewingTarget);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (/*other.GetComponent<HealthSystem>() && */_viewingTarget == null && other.CompareTag(_tagOfTarget))
        {
            Debug.Log("Someone entered to the viewing range" + " TAG: " + other.tag);
            FindedViewTarget?.Invoke(other.transform);
            SetViewingTarget(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == _viewingTarget)
        {
            Debug.Log("Someone left from the viewing range");
            RemoveViewingTarget();
        }
    }

    IEnumerator MinimunViewRemoveDuration(float timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        _viewingTarget = null;
        LostViewTarget?.Invoke();
    }

    private void SetViewingTarget(Transform target)
    {
        _viewingTarget = target;
    }

    private void RemoveViewingTarget()
    {
        StartCoroutine(MinimunViewRemoveDuration(minimumViewDurationInSeconds));
    }
    private void OnDestroy()
    {
        attackController.TargetEnteredToAttackRadius -= SetViewingTarget;
        attackController.CurrentAttackTargetHasLosted -= RemoveViewingTarget;
    }
}
