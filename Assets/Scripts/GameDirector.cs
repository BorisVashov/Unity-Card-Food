using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour 
{
	CardDealer cardDealer;
	ScoreDirector scoreDirector;

	private int CountOfDealedCards = 0;
	private bool gameIsStarted = false; 

	void Update()
	{
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

				cardDealer.PutChoosenCardToPlayerBase();

				CountOfDealedCards -= 2;
			}
			else
			{
				cardDealer.ResetChoosenCards();
			}
		}
	}

	void Start()
	{
		cardDealer = GameObject.Find("CardDealer").GetComponent<CardDealer>();

		scoreDirector = new ScoreDirector();
	}
	public void StartGame () 
	{
		gameIsStarted = true;
	}
	
	public void RestartGame () 
	{
		cardDealer.ResetCards();
	}


}
