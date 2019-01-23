using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour 
{
	public GameObject PlayerBase;
	// public GameObject StartDealPositionGO;
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
		cardPrefab = Resources.Load<GameObject>("CardPrefab");

		cardGenerator = gameObject.GetComponent<CardGenerator>();

		touchController = GameObject.Find("TouchController").GetComponent<TouchController>();

		FoodCards = cardGenerator.GenerateCards(cardPrefab, foodCount);
	}
	
	void Start () 
	{
		PlayerBase = this.transform.Find("PlayerBase").gameObject;

		StartDealPos = this.transform.Find("StartDealPos").transform.position;

		touchController.gameObject.SetActive(false);
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
	
	public void DealCards()
	{
		Debug.Log("Deal length: " + FoodCards.Length);

		ShuffleCards();

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

		yield return new WaitForSeconds(1f);

		for (int id = 0; id < GameRules.CardForDealing; id++)
		{
			FoodCards[roundOfDealing * 20 + id].ShowCard();
		}

		yield return new WaitForSeconds(1f);

		for (int id = 0; id < GameRules.CardForDealing; id++)
		{
			FoodCards[roundOfDealing * 20 + id].HideCard();
		}

		touchController.gameObject.SetActive(true);

		roundOfDealing++;
	}

	public void ResetChoosenCards()
	{
		StartCoroutine(ResetChoosenCardsCoroutine(GameRules.TimeBeforeResetChoosenCards));
	}

	IEnumerator ResetChoosenCardsCoroutine(float timeBeforeResetChoosenCards)
	{
		Card tempFirst = cardFirst;
		Card tempSecond = cardSecond;

		cardFirst = null;
		cardSecond = null;

		yield return new WaitForSeconds(timeBeforeResetChoosenCards);

		tempFirst.ResetCard(isDisableCollider: false);
		tempSecond.ResetCard(isDisableCollider: false);
	}

	private Vector2 GetTargetPosition(int id)
	{
		float x = StartDealPos.x + id % 4 + id % 4 * 0.5f;
		float y = StartDealPos.y - id / 4 - id / 4 * 0.5f;

		Vector2	targetPos = new Vector2(x, y);

		return targetPos;
	}

	public void ResetCards()
	{
		bool isDealing = false;

		for(int id = 0; id < GameRules.CardForDealing; id++)
		{
			Debug.Log("Reset id: " + id);

			FoodCards[id].ResetCard(isDisableCollider: true);
			
			FoodCards[id].MoveCardToPosition(this.transform.position, isDealing);
		}
	}

	public void PutChoosenCardToPlayerBase()
	{
		Card tempFirst = cardFirst;
		Card tempSecond = cardSecond;

		cardFirst = null;
		cardSecond = null;

		tempFirst.MoveCardToPosition(PlayerBase.transform.position, false);
		tempSecond.MoveCardToPosition(PlayerBase.transform.position, false);

		tempFirst.DisableColliderAndTouchedOn();
		tempSecond.DisableColliderAndTouchedOn();

	}

}
