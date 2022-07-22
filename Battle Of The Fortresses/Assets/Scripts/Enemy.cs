using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageAble
{
    [SerializeField] private Transform _viewingTarget;
    [SerializeField] private Transform _attackTarget;

    [Header("Settings")]
    [SerializeField] private float _health;
    [SerializeField] private int _attackDamage;
    [SerializeField] private ViewingRadius _viewingkRadius;
    [SerializeField] private AttackRadius _attackRadius;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackCooldown = 2;
    [SerializeField] private float _attackCDCounter;
    [SerializeField] private float _minChaseTimeInSeconds = 2f;

    private void Start()
    {
        // Viewing radius events subscription: 
        _viewingkRadius.SoldierEnterInAttackRadius += SetViewingTarget;
        _viewingkRadius.CurrentViewTargetWentOutOfViewRange += RemoveViewingTarget;

        // Attack radius events subscription: 
        _attackRadius.TargetEnteredToAttackRadius += SetAttackTarget;
        _attackRadius.CurrentAttackTargetHasLosted += RemoveAttackTarget;

        _attackCDCounter = _attackCooldown;
    }
    private void Update()
    {
        if (_viewingTarget != null)
        {
            StartChase();
        }
    }
    private void StartChase()
    {
        MoveTo(_viewingTarget);
        transform.LookAt(_viewingTarget);

        if (_attackTarget != null && _attackCDCounter <= 0)
        {
            DoAttack(_attackTarget);
        }
        if (_attackCDCounter >= 0)
        {
            _attackCDCounter -= Time.deltaTime;
        }
    }
    private void DoAttack(Transform target)
    {
        Debug.Log("Enemy has attacked: " + target.name);
        _attackCDCounter = _attackCooldown;
    }
    private void RemoveAttackTarget()
    {
        _attackTarget = null;
    }
    private void RemoveViewingTarget()
    {
        StartCoroutine(RemoveViewTargetDuration(_minChaseTimeInSeconds));
    }
    IEnumerator RemoveViewTargetDuration(float removeDurationInSec)
    {
        yield return new WaitForSeconds(removeDurationInSec);
        _viewingTarget = null;
    }
    private void SetAttackTarget(Transform target)
    {
        _viewingTarget = target;
        _attackTarget = target;
    }
    private void SetViewingTarget(Transform target)
    {
        _attackCDCounter = _attackCooldown; // When enemy see soldier attack counter starts
        _viewingTarget = target;
        StartChase();
    }
    public void TakeDamage(float damage)
    {
        _health -= damage;
    }
    private void MoveTo(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
    }
    private void OnDestroy()
    {
        _viewingkRadius.SoldierEnterInAttackRadius -= SetViewingTarget;
        _viewingkRadius.CurrentViewTargetWentOutOfViewRange -= RemoveViewingTarget;

        _attackRadius.TargetEnteredToAttackRadius -= SetAttackTarget;
        _attackRadius.CurrentAttackTargetHasLosted -= RemoveAttackTarget;
    }
}
