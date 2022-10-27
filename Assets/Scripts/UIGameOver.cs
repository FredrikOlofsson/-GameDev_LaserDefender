using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI scoreText;

	void Start()
	{
		scoreText.text = "Score: " + ScoreKeeper.GetScore();
	}
}

