using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] WaveConfigSO waveConfigSO;
    List<Transform> waypoints;


    int waypointIndex = 0;
    void Start()
    {
        waypoints = waveConfigSO.GetWayPoints();
        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        
        print("Current waypoint index: " + waypointIndex + " waypoints.Count : " + waypoints.Count);
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
