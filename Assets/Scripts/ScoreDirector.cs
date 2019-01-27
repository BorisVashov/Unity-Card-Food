using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDirector
{
	private int currentGameScore = 0;

	public void AddScore(int x)
	{
		currentGameScore += x;
	}

	public int GetCurrentGameScore()
	{
		return currentGameScore;
	}

	public void ResetCurrentGameScore()
	{
		currentGameScore = 0;
	}
	
}
