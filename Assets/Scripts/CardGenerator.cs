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

		Sprite frontSide = Resources.Load<Sprite>("FrontSide");
		Sprite backSide = Resources.Load<Sprite>("BackSide");

		Sprite[] sprites = Resources.LoadAll<Sprite>("FoodSprites/food");

		for (int id = 0, index = 0; index < cardArray.Length; id++, index += 2)
		{
			cardArray[index] = Instantiate(cardPrefab, this.transform).GetComponent<Card>();

			imageFood = sprites[id];

			SetupCard(cardArray[index], id, imageFood, frontSide, backSide);

			// ------> вторую такую же карту ложим в колоду
			cardArray[index + 1] = Instantiate(cardPrefab, this.transform).GetComponent<Card>();
			
			SetupCard(cardArray[index + 1], id, imageFood, frontSide, backSide);

			cardArray[index].gameObject.SetActive(false);
			cardArray[index + 1].gameObject.SetActive(false);
		}

		return cardArray;
	}

	private void SetupCard(Card card, int id, Sprite imageFood, Sprite _frontSide, Sprite _backSide)
	{
		GameObject foodSpriteGO = card.transform.Find("FoodSprite").gameObject;

		foodSpriteGO.GetComponent<SpriteRenderer>().sprite = imageFood;
		
		card.CardId = id;

		card.InstalReferences(foodSpriteGO, _frontSide, _backSide);

		card.ResetCard();	
	}

}
