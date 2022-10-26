using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header ("General")]
    [SerializeField] GameObject projectPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float projectileBaseFireRate = 0.2f;

    [Header("AI")]
    [SerializeField] float projectileFireRateVariance = 0.2f;
    [SerializeField] float projectileMinSpawntime = 0.1f;
    [SerializeField] bool useAI;

    public bool isFireing;

    Coroutine fireCoroutine;
    void Start()
    {
        if (useAI == true)
        {
            isFireing = true;
        }
    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFireing && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinously());
        } else if (isFireing == false && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }
 

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            float timeToNextProjectile = Random.Range(projectileBaseFireRate - projectileFireRateVariance,
                                                    projectileBaseFireRate + projectileFireRateVariance);
            math.clamp(timeToNextProjectile, projectileMinSpawntime, projectileBaseFireRate + projectileFireRateVariance);
            Destroy(instance, projectileLifetime);
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
