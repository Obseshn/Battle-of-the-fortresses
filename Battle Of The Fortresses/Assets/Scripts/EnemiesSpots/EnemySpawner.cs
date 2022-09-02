using UnityEngine;
using System;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyUnit[] enemyUnits;
    /*public Action<EnemyUnit> EnemySpawnedEvent;*/

    public void CreateRandomGroupOfEnemiesWithDuration(float durationInSeconds, Transform[] spawnPositions, EnemiesSpotController spotSender)
    {
        StartCoroutine(CreatingEnemiesDurationRoutine(durationInSeconds, spawnPositions, spotSender));
    }

    IEnumerator CreatingEnemiesDurationRoutine(float durationTimeInSec, Transform[] positionsToSpawn, EnemiesSpotController spotSender)
    {
        yield return new WaitForSeconds(durationTimeInSec);
        CreateEnemy(positionsToSpawn, enemyUnits[UnityEngine.Random.Range(0, enemyUnits.Length)], spotSender);
    }
    private void CreateEnemy(Transform[] spawnPositions, EnemyUnit unitToSpawn, EnemiesSpotController spotSender)
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            var prefab = Instantiate(unitToSpawn, spawnPositions[i].position, spawnPositions[i].rotation);
            prefab.GetComponent<EnemyUnit>().spotPosition = spawnPositions[i];
            /*EnemySpawnedEvent?.Invoke(prefab);*/
            spotSender.controlledEnemies.Add(prefab);
        }

    }

}
