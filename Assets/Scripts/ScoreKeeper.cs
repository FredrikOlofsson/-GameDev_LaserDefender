using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreKeeper
{
	private static int score;

	public static int GetScore()
	{
		return score;
	}
	public static void AddScore(int value)
	{
		score += value;
		Mathf.Clamp(score, 0, int.MaxValue);
	}
	public static void ResetScore()
	{
		score = 0;
	}
}
