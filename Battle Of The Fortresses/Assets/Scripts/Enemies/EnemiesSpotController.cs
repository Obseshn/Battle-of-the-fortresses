using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemiesSpotController : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private EnemySpawner enemySpawner;
    BoxCollider spotCollider;

    private void Start()
    {
         spotCollider = gameObject.GetComponent<BoxCollider>();
        enemySpawner.CreateGroupOfTurtle(2);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            StartCoroutine(SetEnemyIsOnSpotState(true, other.GetComponent<Enemy>(), 4));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            StartCoroutine(SetEnemyIsOnSpotState(false, other.GetComponent<Enemy>(), 0));
            if (Physics.OverlapBox(transform.position, spotCollider.size).Length <= 0)
            {
                enemySpawner.CreateGroupOfTurtle(3);
            }
            
        }
    }

    IEnumerator SetEnemyIsOnSpotState(bool state, Enemy enemy, float durationInSec)
    {
        yield return new WaitForSeconds(durationInSec);
        enemy.isOnSpotPosition = state;
    }
}
