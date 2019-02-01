using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDirector : MonoBehaviour
{
	private int currentGameScore = 0;

	private int bestScore;

	public TextMeshProUGUI currentGameTMPro;

	public TextMeshProUGUI bestScoreTMPro;

	void Start()
	{
		InitializeBestScore();
	}

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
		UpdatePlayerPrefsScore();

		currentGameScore = 0;

		UpdateCurrentGameScoreTMPro();
	}

	public void ResetBestScore()
	{
		PlayerPrefs.SetInt("score", 0);
		PlayerPrefs.Save();

		UpdateBestScore(0);
	}

	private void UpdateCurrentGameScoreTMPro()
	{
		currentGameTMPro.text = currentGameScore.ToString();
	}
	
	public void UpdatePlayerPrefsScore()
	{
		// bestScore = PlayerPrefs.GetInt("score", 0);

		if (currentGameScore > bestScore)
		{
			UpdateBestScore(currentGameScore);

			PlayerPrefs.SetInt("score", currentGameScore);
			PlayerPrefs.Save();
		}
	}

	private void InitializeBestScore()
	{
		UpdateBestScore(PlayerPrefs.GetInt("score", 0));
	}

	private void UpdateBestScore(int _currentGameScore)
	{
		bestScore = _currentGameScore;
		bestScoreTMPro.text = bestScore.ToString();
	}

	
}
