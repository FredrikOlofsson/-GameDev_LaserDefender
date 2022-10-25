using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waves;
    WaveConfigSO currentWave;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] bool isLooping;
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
    IEnumerator SpawnEnemyWaves()
    {

        do
        {
            foreach (WaveConfigSO wave in waves)
            {
                currentWave = wave;
                int amountOfEnemies = currentWave.GetEnemyCount();
                for (int i = 0; i < amountOfEnemies; i++)
                {
                    GameObject enemy = currentWave.GetEnemyPrefab(i);
                    Instantiate(enemy, currentWave.getStartingWayPoint().position, quaternion.identity, transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);
    }
    
}
