using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health health;
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;

    void Awake()
    {
        if (health == null)
        {
            health = FindObjectOfType<Health>();
            Debug.Log("Forgot to add object in " + gameObject.name + " inspector, automaticly adding one from : " + health.name);
        }

        healthSlider = GetComponentInChildren<Slider>();
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
    }
    void Start()
    {
        healthSlider.maxValue = health.GetHealth();
    }

    void Update()
    {
        healthSlider.value = health.GetHealth();
        scoreText.text = ScoreKeeper.GetScore().ToString("0000");
    }

}
