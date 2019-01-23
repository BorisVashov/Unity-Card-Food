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

	private CardGenerator cardGenerator;

	void Awake()
	{
		cardPrefab = Resources.Load<GameObject>("CardPrefab");

		cardGenerator = gameObject.GetComponent<CardGenerator>();

		Debug.Log("Try start generate");

		if (cardPrefab != null)
		{
			Debug.Log("Start Generate");

			FoodCards = cardGenerator.GenerateCards(cardPrefab, foodCount);
		}
	
	}
	
	void Start () 
	{
		PlayerBase = this.transform.Find("PlayerBase").gameObject;

		StartDealPos = this.transform.Find("StartDealPos").transform.position;
	}
	
	public void DealCards()
	{
			Debug.Log("Deal length: " + FoodCards.Length);

			StartCoroutine(DealCardsCoroutine(GameRules.TimeBetweenDealCard));
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

	IEnumerator DealCardsCoroutine(float timeBetweenDealCard)
	{
		bool isDealing = true;

		for(int id = 0; id < FoodCards.Length / 2; id++)
		{
			Debug.Log("Deal id: " + id);

			Vector2 targetPosition = GetTargetPosition(id);

			FoodCards[id].MoveCardToPosition(targetPosition, isDealing);

			/*if ((id + 1) % 5 == 0 && id != 0) // ---->> механика раздачи карт
			{
				yield return new WaitForSeconds(timeBetweenDealCard * 3);
			}
			else*/
			{
				yield return new WaitForSeconds(timeBetweenDealCard);
			}
		}
	}

	private Vector2 GetTargetPosition(int id)
	{
		float x = StartDealPos.x + id % 5 + id % 5 * 0.3f;
		float y = StartDealPos.y - id / 5 - id / 5 * 0.3f;

		Vector2	targetPos = new Vector2(x, y);

		return targetPos;
	}

	public void ResetCards()
	{
		bool isDealing = false;

		for(int id = 0; id < FoodCards.Length / 2; id++)
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
