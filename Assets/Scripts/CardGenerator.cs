using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGenerator : MonoBehaviour
{
	private GameObject cardPrefab;
	private int foodCount = 30; // количество видов еды

	public Card[] FoodCards;

	void Awake()
	{
		cardPrefab = Resources.Load<GameObject>("CardPrefab");

		Debug.Log("Try start generate");

		if (cardPrefab != null)
		{
			Debug.Log("Start Generate");

			FoodCards = GenerateCards(cardPrefab, foodCount);
		}
	
	}

	public Card[] GenerateCards(GameObject cardPrefab, int foodCount)
	{
		Card[] cardArray = new Card[foodCount * 2];

		GameObject imageFood;

		for (int id = 0, index = 0; index < cardArray.Length; id++, index += 2)
		{
			cardArray[index] = Instantiate(cardPrefab, this.transform).GetComponent<Card>();

			imageFood = Resources.Load<GameObject>("Food/food_" + id);

			SetupCard(cardArray[index], id, imageFood);

			// ------> вторую такую же карту ложим в колоду
			cardArray[index + 1] = Instantiate(cardPrefab, this.transform).GetComponent<Card>();
			
			SetupCard(cardArray[index + 1], id, imageFood);
		}

		return cardArray;
	}

	private void SetupCard(Card card, int id, GameObject imageFood)
	{

		Instantiate(imageFood, card.transform.position, card.transform.rotation, card.transform);
		// GameObject foodImageGO = cardArray[id].transform.Find("food_" + id + "(Clone)").gameObject;
		GameObject foodImageGO = card.transform.Find("food_" + id + "(Clone)").gameObject;
		GameObject backSideGO = card.transform.Find("BackSide").gameObject;
		GameObject frontSideGO = card.transform.Find("FrontSide").gameObject;

		foodImageGO.name = "FoodImage";

		card.CardId = id;

		card.InstalReferences(foodImageGO, frontSideGO, backSideGO);

		card.ResetCard();	
	}

}
