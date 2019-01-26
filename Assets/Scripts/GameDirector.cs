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

	void Start()
	{
		cardDealer = GameObject.Find("CardDealer").GetComponent<CardDealer>();

		scoreDirector = new ScoreDirector();
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

		lives = GameRules.LivesForGame;

		Debug.Log("Lives = " + lives);

		gameIsStarted = true;
	}
	
	public void RestartGame () 
	{
		cardDealer.ResetCards();
	}

	public void StopGame()
	{

		gameIsStarted = false;

		// cardDealer.cardFirst = null;
		// cardDealer.cardSecond = null;

		cardDealer.GetBackAllCardsToDeck();

		mainMenuUI.SetActive(true);

		CountOfDealedCards = 0;


	}


}
