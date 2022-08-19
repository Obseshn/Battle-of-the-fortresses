using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemiesSpotController : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private LayerMask armyLayer;
    [SerializeField] private float observerRadius;
    [SerializeField] private Transform[] enemiesPositions;

    private readonly List<EnemyUnit> controlledEnemies = new List<EnemyUnit>();
    private readonly List<EnemyUnit> enemiesWithoutTarget = new List<EnemyUnit>();
    private readonly List<Transform> emptySpotPositions = new List<Transform>();

    [SerializeField] private float attackRadiusCounter;
    private float checkAttackRadiusTime = 4f;

    BoxCollider spotCollider;
    private bool isOnAttack = false;

    
    private void Start()
    {
         emptySpotPositions.AddRange(enemiesPositions.ToList());
         spotCollider = gameObject.GetComponent<BoxCollider>();
         enemySpawner.CreateGroupOfTurtle(3);
         enemySpawner.EnemySpawnedEvent += AddEnemyToSpot;
    }

    private void Update()
    {
        Debug.Log("Spot controlled: " + controlledEnemies.Count);
        if (attackRadiusCounter <= 0)
        {
            if (isOnAttack)
            {
                Collider[] soldiersInObserver = Physics.OverlapSphere(transform.position, observerRadius, armyLayer);

                if (controlledEnemies.Count > 0)
                {
                    foreach (var unit in controlledEnemies)
                    {
                        if (unit.GetComponent<EnemyUnit>().ViewingTarget == null && enemiesWithoutTarget.Contains(unit) == false)
                        {
                            enemiesWithoutTarget.Add(unit);
                        }
                    }

                    if (soldiersInObserver.Length != 0 && enemiesWithoutTarget.Count > 0)
                    {
                        foreach (var enemy in controlledEnemies)
                        {
                            enemy.GetComponent<EnemyUnit>().SetViewingTarget(soldiersInObserver[Random.Range(0, soldiersInObserver.Length)].transform);
                        }
                        attackRadiusCounter = checkAttackRadiusTime;
                    }
                    else
                    {
                        foreach (var enemy in controlledEnemies)
                        {
                            enemy.RemoveViewingTarget();
                            enemy.SwitchIsOnSpotPosBool(false);
                        }
                    }
                }
            }
        }
        else if (attackRadiusCounter >= 0)
        {
            attackRadiusCounter -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Army") && !isOnAttack)
        {
            isOnAttack = true;
        }

        if (other.GetComponent<EnemyUnit>())
        {
            StartCoroutine(SetEnemyIsOnSpotState(true, other.GetComponent<EnemyUnit>(), 4));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyUnit>())
        {
            StartCoroutine(SetEnemyIsOnSpotState(false, other.GetComponent<EnemyUnit>(), 0));
            if (Physics.OverlapBox(transform.position, spotCollider.size).Length <= 0)
            {
                enemySpawner.CreateGroupOfTurtle(3);
            }
        }
    }

    IEnumerator SetEnemyIsOnSpotState(bool state, EnemyUnit enemy, float durationInSec)
    {
        yield return new WaitForSeconds(durationInSec);
        enemy.SwitchIsOnSpotPosBool(state);
    }
    private void AddEnemyToSpot(EnemyUnit enemy)
    {
        controlledEnemies.Add(enemy);
        int index = Random.Range(0, emptySpotPositions.Count);
        enemy.spotPosition = emptySpotPositions[index];
        emptySpotPositions.Remove(emptySpotPositions[index]);
        enemy.enemyDied += RemoveEnemyFromSpot;
    }

    private void RemoveEnemyFromSpot(EnemyUnit enemy)
    {
        controlledEnemies.Remove(enemy);
        emptySpotPositions.Add(enemy.spotPosition);
        enemy.enemyDied -= RemoveEnemyFromSpot;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, observerRadius);
    }
}
