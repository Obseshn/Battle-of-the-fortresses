using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemiesSpotController : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask armyLayer;
    [SerializeField] private float observerRadius;
    [SerializeField] private Transform[] enemiesPositions;

    public readonly List<EnemyUnit> controlledEnemies = new List<EnemyUnit>();
    private readonly List<EnemyUnit> enemiesWithoutTarget = new List<EnemyUnit>();
    private readonly List<Transform> emptySpotPositions = new List<Transform>();

    [SerializeField] private float attackRadiusCounter;
    private float checkAttackRadiusTime = 4f;

    BoxCollider spotCollider;
    private bool isOnAttack = false;

    
    private void Start()
    {
/*        enemySpawner.EnemySpawnedEvent += AddEnemyToSpot;*/
        emptySpotPositions.AddRange(enemiesPositions.ToList());
        spotCollider = gameObject.GetComponent<BoxCollider>();
        enemySpawner.CreateRandomGroupOfEnemiesWithDuration(0f, enemiesPositions, this);
    }

    private void Update()
    {
        Debug.Log("Spot controlled: " + controlledEnemies.Count + transform.name);
        if (attackRadiusCounter <= 0)
        {
            if (isOnAttack)
            {
                Debug.Log("spot: " + transform.name + " is under attack!");
                Collider[] soldiersInObserver = Physics.OverlapSphere(transform.position, observerRadius, armyLayer);

                Debug.Log("Controlled enemies count: " + controlledEnemies.Count);
                if (controlledEnemies.Count > 0)
                {
                    Debug.Log("Controlled enemies > 0");
                    foreach (var unit in controlledEnemies)
                    {
                        if (unit.GetComponent<EnemyUnit>().ViewingTarget == null && enemiesWithoutTarget.Contains(unit) == false)
                        {
                            enemiesWithoutTarget.Add(unit);
                            Debug.Log("Added new enemy without target");
                        }
                    }

                    if (soldiersInObserver.Length != 0 && enemiesWithoutTarget.Count > 0)
                    {
                        foreach (var enemy in enemiesWithoutTarget)
                        {

                            enemy.GetComponent<EnemyUnit>().SetViewingTarget(soldiersInObserver[Random.Range(0, soldiersInObserver.Length)].transform);
                            Debug.Log("Set view target for enemy!");
                        }
                        enemiesWithoutTarget.Clear();
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
        else if (attackRadiusCounter > 0)
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
            if (Physics.OverlapSphere(transform.position, observerRadius, enemyLayer).Length <= 1)
            {
                Debug.Log("Camp had been stucked");
                enemySpawner.CreateRandomGroupOfEnemiesWithDuration(3f, enemiesPositions, this); // 3 - duration
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
        Debug.Log(enemy.name + "has been added to: " + transform.name);
        emptySpotPositions.Remove(enemy.spotPosition);
        enemy.EnemyDied += RemoveEnemyFromSpot;
    }

    private void RemoveEnemyFromSpot(EnemyUnit enemy)
    {
        controlledEnemies.Remove(enemy); 
        emptySpotPositions.Add(enemy.spotPosition);
        enemy.EnemyDied -= RemoveEnemyFromSpot;
        if (controlledEnemies.Count == 0)
        {
            enemySpawner.CreateRandomGroupOfEnemiesWithDuration(15f, enemiesPositions, this); // 15 - duration
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, observerRadius);
    }
}
