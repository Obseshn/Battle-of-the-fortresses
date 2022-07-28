using UnityEngine;
using System;
using System.Collections;
public class AggroController : MonoBehaviour
{
    [SerializeField] private string _tagOfTarget;

    [SerializeField] private Transform _chaseTarget;

    [SerializeField] private float _moveSpeed = 2f;

    private readonly float minimumChaseDurationInSeconds = 2f;

    /*private event FindTarget SoldierEnterInAttackRadius;
    private delegate void FindTarget(Transform target);

    private event Action CurrentViewTargetWentOutOfViewRange;*/

    private void Start()
    {
        /*SoldierEnterInAttackRadius += SetChaseTarget;
        CurrentViewTargetWentOutOfViewRange += RemoveChaseTarget;*/
    }

    private void Update()
    {
        if (_chaseTarget != null)
        {
            StartChase();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthSystem>() && _chaseTarget == null && other.CompareTag(_tagOfTarget))
        {
            Debug.Log("Someone entered to the viewing range" + "TAG: " + other.tag);
            /*SoldierEnterInAttackRadius?.Invoke(other.transform);*/
            SetChaseTarget(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == _chaseTarget)
        {
            Debug.Log("Someone left from the viewing range");
            /*CurrentViewTargetWentOutOfViewRange?.Invoke();*/
            RemoveChaseTarget();
        }
    }
    private void SetChaseTarget(Transform target)
    {
        _chaseTarget = target;
        Debug.Log(_chaseTarget.name);
        
    }

    IEnumerator MinimunChaseRemoveDuration(float timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        _chaseTarget = null;
    }

    private void RemoveChaseTarget()
    {
        StartCoroutine(MinimunChaseRemoveDuration(minimumChaseDurationInSeconds));
    }
    private void StartChase()
    {
        MoveTo(_chaseTarget.transform);
        transform.parent.LookAt(_chaseTarget.transform);
    }

    private void MoveTo(Transform target)
    {
        transform.parent.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
    }

}
