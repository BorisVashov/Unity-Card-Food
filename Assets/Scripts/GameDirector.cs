using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour 
{
	CardDealer cardDealer;

	public static PlayerState Players;

	void Update()
	{
		if (cardDealer.cardFirst != null && cardDealer.cardSecond != null)
		{
			if (cardDealer.cardFirst.CardId == cardDealer.cardSecond.CardId)
			{
				cardDealer.cardFirst.MoveCardToPosition(cardDealer.PlayerBase.transform.position, false);
				cardDealer.cardSecond.MoveCardToPosition(cardDealer.PlayerBase.transform.position, false);
			}
			else
			{
				cardDealer.cardFirst = null;
				cardDealer.cardSecond = null;

				cardDealer.ResetChoosenCards();
			}
		}
	}

	void Start()
	{
		cardDealer = GameObject.Find("CardDealer").GetComponent<CardDealer>();
	}
	public void StartGame () 
	{
		Debug.Log("StartGame");
		
		cardDealer.DealCards();
	}
	
	public void RestartGame () 
	{
		cardDealer.ResetCards();
	}


}
