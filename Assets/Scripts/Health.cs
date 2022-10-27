using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem explosionEffect;

    [SerializeField] bool shouldShake;
    CameraShake camShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    public int GetHealth()
    {
        return health;
    }
    void Awake()
    {
        camShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            PlayExplosion();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }
    }
    private void TakeDamage(int damage)
    {
        health -= damage;
        ShakeCamera();
        if (health <= 0) Die();
    }
    private void Die()
    {
        if (isPlayer == false)
        {
            scoreKeeper.AddScore(score);
        }
        if (isPlayer == true)
        {
            camShake.shakeDuration *= 2;
            camShake.shakeMagnitude *= 2;
            ShakeCamera();
        }
        Destroy(gameObject);

    }
    private void PlayExplosion()
    {
        if (explosionEffect != null)
        {
            ParticleSystem instance = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
    private void ShakeCamera()
    {
        if (camShake != null && shouldShake)
        {
            camShake.Play();
        }
    }

}
