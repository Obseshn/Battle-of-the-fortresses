using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ViewingController : MonoBehaviour
{
    [SerializeField] public string tagOfTarget;
    [SerializeField] private Transform viewingTarget;

    private readonly float minimumViewDurationInSeconds = 2f;

    public event Action<Transform> FindedViewTarget;
    public event Action LostViewTarget;

    private void Start()
    {
        gameObject.GetComponent<SphereCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (viewingTarget == null && other.CompareTag(tagOfTarget))
        {
            Debug.Log("Someone entered to the viewing range" + " TAG: " + other.tag);
            SetViewingTarget(other.transform);
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
        FindedViewTarget?.Invoke(target);
        viewingTarget = target;
    }

    private void RemoveViewingTarget()
    {
        StartCoroutine(MinimunViewRemoveDuration(minimumViewDurationInSeconds));
    }
}
