using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] public float projectileBaseFireRatePerSec = 5f;

    [Header("AI")]
    [SerializeField] float projectileFireRateVariance = 0.2f;
    [SerializeField] float projectileMinSpawntime = 0.1f;
    [SerializeField] bool useAI;

    AudioPlayer audioPlayer;
    Coroutine fireCoroutine;
    public bool isFireing;
    private float lastShot;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

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
        /*
        if (isFireing && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinously());
        } else if (isFireing == false && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }*/
        if (isFireing == true && lastShot < Time.time)
        {
            lastShot = Time.time + (1/projectileBaseFireRatePerSec);
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject instance = Instantiate(projectPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * projectileSpeed;

        audioPlayer.PlayShootingClip();
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
            Destroy(instance, projectileLifetime);

            float timeToNextProjectile = Random.Range(projectileBaseFireRatePerSec - projectileFireRateVariance,
                                                    projectileBaseFireRatePerSec + projectileFireRateVariance);
            timeToNextProjectile = math.clamp(timeToNextProjectile, projectileMinSpawntime, projectileBaseFireRatePerSec + projectileFireRateVariance);

            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
