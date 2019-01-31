using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGenerator : MonoBehaviour
{
	public Card[] GenerateCards(GameObject cardPrefab, int foodCount)
	{
		Card[] cardArray = new Card[foodCount * 2];

		Sprite imageFood;

		Sprite backSide = Resources.Load<Sprite>("BackSide");

		Sprite[] frontSides = Resources.LoadAll<Sprite>("FoodSprites/food");

		for (int id = 0, index = 0; index < cardArray.Length; id++, index += 2)
		{
			cardArray[index] = Instantiate(cardPrefab, this.transform).GetComponent<Card>();

			imageFood = frontSides[id];

			SetupCard(cardArray[index], id, imageFood, backSide);

			// ------> вторую такую же карту ложим в колоду
			cardArray[index + 1] = Instantiate(cardPrefab, this.transform).GetComponent<Card>();
			
			SetupCard(cardArray[index + 1], id, imageFood, backSide);

			cardArray[index].gameObject.SetActive(false);
			cardArray[index + 1].gameObject.SetActive(false);
		}

		return cardArray;
	}

	private void SetupCard(Card card, int id, Sprite imageFood, Sprite _backSide)
	{
		card.CardId = id;

		card.InstalReferences(imageFood, _backSide);

		card.ResetCard();	
	}

}
