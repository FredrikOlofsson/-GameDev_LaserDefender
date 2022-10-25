using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "Wave ")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenSpawns = 1f;
    [SerializeField] float spawnTimevariance = 0.3f;
    [SerializeField] float minimumSpawnTime = 0.2f;

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }
    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }
    public Transform getStartingWayPoint()
    {
        return pathPrefab.GetChild(0);
    }
    public List<Transform> GetWayPoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform item in pathPrefab)
        {
            waypoints.Add(item);
        }
        return waypoints;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public float GetRandomSpawnTime()
    {
        float randomTime = Random.Range(timeBetweenSpawns - spawnTimevariance,
                                        timeBetweenSpawns + spawnTimevariance);
        return Mathf.Clamp(randomTime, minimumSpawnTime, float.MaxValue);
    }
}
