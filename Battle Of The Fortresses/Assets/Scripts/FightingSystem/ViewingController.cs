using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ViewingController : MonoBehaviour
{
    [SerializeField] public string tagOfTarget;
    [SerializeField] private Transform viewingTarget;
    [SerializeField] private ChaseController chaseController;

    private readonly float minimumViewDurationInSeconds = 2f;

    public event Action<Transform> FindedViewTarget;
    public event Action LostViewTarget;

    private void Update()
    {
        if (viewingTarget != null)
        {
            transform.parent.LookAt(viewingTarget);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (viewingTarget == null && other.CompareTag(tagOfTarget))
        {
            if (other.GetComponent<HealthSystem>())
            {
                Debug.Log("Someone entered to the viewing range" + " TAG: " + other.tag);
                FindedViewTarget?.Invoke(other.transform);
                SetViewingTarget(other.transform);
            }
            else
            {
                Debug.LogError("Current target hasn't \"Health System\" component on it!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == viewingTarget)
        {
            Debug.Log("Someone left from the viewing range");
            RemoveViewingTarget();
        }
    }

    IEnumerator MinimunViewRemoveDuration(float timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        viewingTarget = null;
        LostViewTarget?.Invoke();
    }

    public void SetViewingTarget(Transform target)
    {
        viewingTarget = target;
    }

    private void RemoveViewingTarget()
    {
        StartCoroutine(MinimunViewRemoveDuration(minimumViewDurationInSeconds));
    }
}
