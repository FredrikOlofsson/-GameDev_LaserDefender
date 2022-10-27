using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
	private int score;

	public int GetScore()
	{
		return score;
	}
	public void AddScore(int value)
	{
		score += value;
		Mathf.Clamp(score, 0, int.MaxValue);
	}
	public void ResetScore()
	{
		score = 0;
	}
}
