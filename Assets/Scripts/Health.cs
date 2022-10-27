using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem explosionEffect;

    [SerializeField] bool shouldShake;
    CameraShake camShake;
    AudioPlayer audioPlayer;
    LevelManager levelManager;
    public int GetHealth()
    {
        return health;
    }
    void Awake()
    {
        camShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        levelManager = FindObjectOfType<LevelManager>();
        
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
        if (health <= 0) StartCoroutine(Die());
    }
    IEnumerator Die()
    {
        if (isPlayer == false)
        {
            ScoreKeeper.AddScore(score);
        }
        if (isPlayer == true)
        {
            float violentShakeDuration = camShake.shakeDuration * 2;
            float violentMagnitude = camShake.shakeDuration * 2;
            camShake.shakeDuration = violentShakeDuration;
            camShake.shakeMagnitude = violentMagnitude;
            ShakeCamera();

            GetComponentInChildren<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(violentShakeDuration);
            Destroy(gameObject);            
            SceneManager.LoadScene("GameOver");
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
