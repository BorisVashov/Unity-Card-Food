using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDirector : MonoBehaviour
{
	private int currentGameScore = 0;

	public TextMeshProUGUI currentGameTMPro;



	public void AddScore(int x)
	{
		currentGameScore += x;

		UpdateCurrentGameScoreTMPro();
	}

	public int GetCurrentGameScore()
	{
		return currentGameScore;
	}

	public void ResetCurrentGameScore()
	{
		currentGameScore = 0;

		UpdateCurrentGameScoreTMPro();
	}

	private void UpdateCurrentGameScoreTMPro()
	{
		currentGameTMPro.text = currentGameScore.ToString();
	}
	
}
