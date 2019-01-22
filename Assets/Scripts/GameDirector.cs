using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour 
{
	CardDealer cardDealer;

	public static PlayerState Players;

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
