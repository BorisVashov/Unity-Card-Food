using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour 
{
	public GameObject startPositionGO;
	Card[] FoodCards;

	Vector2 startDealPos;

	void Start () 
	{
		FoodCards = GameObject.Find("DeckOfCards").GetComponent<CardGenerator>().FoodCards;
		startDealPos = startPositionGO.transform.position;
	}
	
	public void DealCards()
	{
			Debug.Log("Deal length: " + FoodCards.Length);

			StartCoroutine(DealCardsCoroutine(GameRules.TimeBetweenDealCard));
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
		float x = startDealPos.x + id % 5 + id % 5 * 0.3f;
		float y = startDealPos.y - id / 5 - id / 5 * 0.3f;

		Vector2	targetPos = new Vector2(x, y);

		return targetPos;
	}

	public void ResetCards()
	{
		bool isDealing = false;

		for(int id = 0; id < FoodCards.Length / 2; id++)
		{
			Debug.Log("Reset id: " + id);

			FoodCards[id].ResetCard();
			
			FoodCards[id].MoveCardToPosition(this.transform.position, isDealing);
		}
	}

	void Update () 
	{
		
	}
}
