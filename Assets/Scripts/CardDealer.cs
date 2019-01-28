using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour 
{
	public Vector2 StartDealPos;

	Card[] FoodCards;

	public Card cardFirst;
	public Card cardSecond;

	private GameObject cardPrefab;
	private int foodCount = 30; // количество видов еды
	private int roundOfDealing = 0;

	private CardGenerator cardGenerator;

	private TouchController touchController;

	

	void Awake()
	{
		cardPrefab = Resources.Load<GameObject>("CardPrefab4");

		cardGenerator = gameObject.GetComponent<CardGenerator>();

		touchController = GameObject.Find("TouchController").GetComponent<TouchController>();

		FoodCards = cardGenerator.GenerateCards(cardPrefab, foodCount);
	}
	
	void Start () 
	{
		StartDealPos = this.transform.Find("StartDealPos").transform.position;
	}

	private void ShuffleCards()
	{
		for (int id = 0; id < GameRules.CardForDealing; id++)
		{
			int randIndex = 
				Random.Range(roundOfDealing * GameRules.CardForDealing, roundOfDealing * GameRules.CardForDealing + GameRules.CardForDealing);
			
			Card tempCard = FoodCards[randIndex];

			FoodCards[randIndex] = FoodCards[roundOfDealing * GameRules.CardForDealing + id];
			FoodCards[roundOfDealing * GameRules.CardForDealing + id] = tempCard;

			
		}
	}

	private void SetOrderInLayerForCards()
	{
		for (int id = 0; id < GameRules.CardForDealing; id++)
		{
			FoodCards[roundOfDealing * GameRules.CardForDealing + id].spriteRenderer.sortingOrder = id;
		}
	}

	public void ActivateAllCardsObjects()
	{
		for (int id = 0; id < FoodCards.Length; id++)
		{
			FoodCards[id].gameObject.SetActive(true);
		}
	}
	
	public void DealCards()
	{
		touchController.enabled = false;

		Debug.Log("Deal length: " + FoodCards.Length);

		if (roundOfDealing == 3)
		{
			roundOfDealing = 0;
		}

		ShuffleCards();
		ShuffleCards();


		SetOrderInLayerForCards();

		ActivateAllCardsObjects();

		StartCoroutine(DealCardsCoroutine(GameRules.TimeBetweenDealCard));
	}

	IEnumerator DealCardsCoroutine(float timeBetweenDealCard)
	{
		bool isDealing = true;

		for(int id = 0; id < GameRules.CardForDealing; id++)
		{
			Debug.Log("Deal id: " + (id + roundOfDealing * GameRules.CardForDealing));

			Vector2 targetPosition = GetTargetPosition(id);

			FoodCards[roundOfDealing * GameRules.CardForDealing + id].MoveCardToPosition(targetPosition, isDealing);

			if ((id + 1) % 4 == 0 && id != 0) // ---->> механика раздачи карт
			{
				yield return new WaitForSeconds(timeBetweenDealCard * 2);
			}
			else
			{
				yield return new WaitForSeconds(timeBetweenDealCard);
			}
		}

		yield return new WaitForSeconds(timeBetweenDealCard * 5);

		for (int id = 0; id < GameRules.CardForDealing; id++)
		{
			FoodCards[roundOfDealing * 20 + id].ShowCard();
		}

		yield return new WaitForSeconds(GameRules.TimeToShowCardsAtStartRound);

		for (int id = 0; id < GameRules.CardForDealing; id++)
		{
			FoodCards[roundOfDealing * 20 + id].HideCard();
		}

		touchController.enabled = true;

		roundOfDealing++;
	}

	public void HideChoosenCards()
	{
		StartCoroutine(HideChoosenCardsCoroutine(GameRules.TimeBeforeResetChoosenCards));
	}

	IEnumerator HideChoosenCardsCoroutine(float timeBeforeHideChoosenCards)
	{
		Card tempFirst = cardFirst;
		Card tempSecond = cardSecond;

		cardFirst = null;
		cardSecond = null;

		yield return new WaitForSeconds(timeBeforeHideChoosenCards);

		tempFirst.HideCard();
		tempSecond.HideCard();
	}

	private Vector2 GetTargetPosition(int id)
	{
		float x = StartDealPos.x + id % 4 + id % 4 * 0.5f;
		float y = StartDealPos.y - id / 4 - id / 4 * 0.6f;

		Vector2	targetPos = new Vector2(x, y);

		return targetPos;
	}

	public void ResetCards()
	{
		bool isDealing = false;

		for(int id = 0; id < GameRules.CardForDealing; id++)
		{
			Debug.Log("Reset id: " + id);

			FoodCards[id + GameRules.CardForDealing * roundOfDealing].ResetCard();
			
			FoodCards[id + GameRules.CardForDealing * roundOfDealing].MoveCardToPosition(this.transform.position, isDealing);
		}

		roundOfDealing--;
	}

	public void PutChoosenCardsBackToDeck()
	{
		Card tempFirst = cardFirst;
		Card tempSecond = cardSecond;

		cardFirst = null;
		cardSecond = null;

		tempFirst.AddCardToScore();
		tempSecond.AddCardToScore();
	}

	public void GetBackAllCardsToDeck()
	{
		for(int id = 0; id < FoodCards.Length; id++)
		{
			FoodCards[id].GetBackCardToDeck();
		}

		cardFirst = null;
		cardSecond = null;

		roundOfDealing = 0;
	}

	public void StopAllCoroutinesInCards()
	{
		for(int i = 0; i < FoodCards.Length; i++)
		{
			FoodCards[i].StopAllCoroutines();
		}
	}

}
