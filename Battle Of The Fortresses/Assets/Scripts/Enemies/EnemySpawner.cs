using UnityEngine;
using System.Collections;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyTurtleFactory enemyTurtleFactory;
    public Action<EnemyUnit> EnemySpawnedEvent;
    public void CreateGroupOfTurtle(float countOfTurtles)
    {
        for (int i = 0; i < countOfTurtles; i++)
        {
            StartCoroutine(CreateTurtleDelay(i));
        }
    }
    IEnumerator CreateTurtleDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        var prefab = enemyTurtleFactory.GetNewInstance();
        prefab.GetComponent<EnemyUnit>().spotPosition = enemyTurtleFactory._spawnPoint;
        EnemySpawnedEvent?.Invoke(prefab);
    }

}
