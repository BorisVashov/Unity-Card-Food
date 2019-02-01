using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour 
{
	CardDealer cardDealer;
	ScoreDirector scoreDirector;

	private int CountOfDealedCards = 0;
	private bool gameIsStarted = false; 

	private int lives;

	public GameObject mainMenuUI;
	public GameObject gameUI;
	public GameObject gamePauseMenu;
	public GameObject gameAndPauseCanvas;
	public GameObject gameLoseMenu;
	
	public TouchController touchController;
	private bool lastTouchControllerState = false;

	public Image[] starImages;

	private Sprite starOnSprite;
	private Sprite starOffSprite;


	private int bonusAnswers = 0;

	void Start()
	{

		starOnSprite = Resources.Load<Sprite>("StarSprites/star_on");
		starOffSprite = Resources.Load<Sprite>("StarSprites/star_off");

		touchController = GameObject.Find("TouchController").GetComponent<TouchController>();

		cardDealer = GameObject.Find("CardDealer").GetComponent<CardDealer>();

		scoreDirector = GameObject.Find("ScoreDirector").GetComponent<ScoreDirector>();
	}

	void Update()
	{
		if (lives == 0 && gameIsStarted)
		{
			LoseGame();
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
				bonusAnswers++;
				if (bonusAnswers == 1)
				{
					AddBonuseLive();
				}

				scoreDirector.AddScore(1);

				cardDealer.PutChoosenCardsBackToDeck();

				CountOfDealedCards -= 2;
			}
			else
			{
				bonusAnswers = 0; // сброс бонусной серии

				lives--;
				DeleteStar();

				Debug.Log("Oops! Lives = " + lives);

				cardDealer.HideChoosenCards();
			}
		}
	}

	public void StartGame () 
	{
		mainMenuUI.SetActive(false);
		gameUI.SetActive(true);

		gameAndPauseCanvas.SetActive(true);

		lives = GameRules.MaxLivesForGame;

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

		gamePauseMenu.SetActive(false);
		gameUI.SetActive(true);
		gameLoseMenu.SetActive(false);

		AddAllStars();

		gameIsStarted = true;
		lives = GameRules.MaxLivesForGame;
	}

	private void LoseGame()
	{
		gameIsStarted = false;

		scoreDirector.UpdatePlayerPrefsScore();

		touchController.enabled = false;

		Time.timeScale = 0f;
		
		gameLoseMenu.SetActive(true);
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
		gamePauseMenu.SetActive(false);
		gameAndPauseCanvas.SetActive(false);
		gameLoseMenu.SetActive(false);


		CountOfDealedCards = 0;

		AddAllStars();
	}

	public void PauseGame()
	{
		lastTouchControllerState = touchController.enabled;

		touchController.enabled = false;

		Time.timeScale = 0f;
		
		gamePauseMenu.SetActive(true);
		gameUI.SetActive(false);
	}

	public void ContinueGame()
	{
		touchController.enabled = lastTouchControllerState;

		Time.timeScale = 1f;

		gamePauseMenu.SetActive(false);
		gameUI.SetActive(true);
	}

	private void AddBonuseLive()
	{
		if (lives < GameRules.MaxLivesForGame)
		{
			AddStar();
			lives++;
		}


		bonusAnswers = 0;
	}

	private void AddStar()
	{
		starImages[lives].sprite = starOnSprite;
	}

	private void DeleteStar()
	{
		starImages[lives].sprite = starOffSprite;
	}

	private void AddAllStars()
	{
		for (int i = 0; i < starImages.Length; i++)
		{
			starImages[i].sprite = starOnSprite;
		}
	}


}
