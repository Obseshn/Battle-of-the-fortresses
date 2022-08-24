using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyUnit[] enemyUnits;
    public Action<EnemyUnit> EnemySpawnedEvent;

    public void CreateRandomGroupOfEnemies(Transform[] spawnPositions)
    {
        CreateEnemy(spawnPositions, enemyUnits[UnityEngine.Random.Range(0, enemyUnits.Length)]);
    }
    private void CreateEnemy(Transform[] spawnPositions, EnemyUnit unitToSpawn)
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            var prefab = Instantiate(unitToSpawn, spawnPositions[i].position, spawnPositions[i].rotation);
            prefab.GetComponent<EnemyUnit>().spotPosition = spawnPositions[i];
            EnemySpawnedEvent?.Invoke(prefab);
        }
    }

}
