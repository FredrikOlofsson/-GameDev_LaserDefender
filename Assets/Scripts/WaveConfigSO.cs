using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;

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
    public float getMoveSpeed()
    {
        return moveSpeed;
    }
}