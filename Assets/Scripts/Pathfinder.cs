using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    EnemySpawner EnemySpawner;
    WaveConfigSO waveConfigSO;
    List<Transform> waypoints;


    int waypointIndex = 0;
    void Awake()
    {
        EnemySpawner = FindObjectOfType<EnemySpawner>();
    }
    void Start()
    {
        waveConfigSO = EnemySpawner.GetCurrentWave();
        waypoints = waveConfigSO.GetWayPoints();
        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfigSO.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        } else
        {
            Destroy(gameObject);
        }
    }

}
