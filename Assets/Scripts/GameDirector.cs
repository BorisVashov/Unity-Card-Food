using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour 
{
	CardDealer cardDealer;
	ScoreDirector scoreDirector;

	private int CountOfDealedCards = 0;
	private bool gameIsStarted = false; 

	private int lives;

	public GameObject mainMenuUI;
	public GameObject gameUI;
	public GameObject gamePauseUI;
	
	public TouchController touchController;

	void Start()
	{
		touchController = GameObject.Find("TouchController").GetComponent<TouchController>();

		cardDealer = GameObject.Find("CardDealer").GetComponent<CardDealer>();

		scoreDirector = GameObject.Find("ScoreDirector").GetComponent<ScoreDirector>();
	}

	void Update()
	{
		if (lives == 0 && gameIsStarted)
		{
			StopGame();
		}

		if (CountOfDealedCards == 0 && gameIsStarted)
		{
			CountOfDealedCards = GameRules.CardForDealing;

			cardDealer.DealCards();
		}

		if (cardDealer.cardFirst != null && cardDealer.cardSecond != null)
		{
			if (cardDealer.cardFirst.CardId == cardDealer.cardSecond.CardId)
			{
				scoreDirector.AddScore(1);

				cardDealer.PutChoosenCardsBackToDeck();

				CountOfDealedCards -= 2;
			}
			else
			{
				lives--;
				Debug.Log("Oops! Lives = " + lives);

				cardDealer.HideChoosenCards();
			}
		}
	}

	public void StartGame () 
	{
		mainMenuUI.SetActive(false);
		gameUI.SetActive(true);

		lives = GameRules.LivesForGame;

		Debug.Log("Lives = " + lives);

		gameIsStarted = true;
	}
	
	public void RestartGame () 
	{
		cardDealer.StopAllCoroutines();

		scoreDirector.ResetCurrentGameScore();

		Time.timeScale = 1f;

		cardDealer.GetBackAllCardsToDeck();

		CountOfDealedCards = 0;

		gamePauseUI.SetActive(false);
		gameUI.SetActive(true);
	}

	public void StopGame()
	{
		cardDealer.StopAllCoroutines();

		scoreDirector.ResetCurrentGameScore();

		Time.timeScale = 1f;

		gameIsStarted = false;

		cardDealer.GetBackAllCardsToDeck();

		mainMenuUI.SetActive(true);
		gameUI.SetActive(false);
		gamePauseUI.SetActive(false);

		CountOfDealedCards = 0;
	}

	public void PauseGame()
	{
		touchController.enabled = false;

		Time.timeScale = 0f;
		
		gamePauseUI.SetActive(true);
		gameUI.SetActive(false);
	}

	public void ContinueGame()
	{
		touchController.enabled = true;

		Time.timeScale = 1f;

		gamePauseUI.SetActive(false);
		gameUI.SetActive(true);
	}


}
