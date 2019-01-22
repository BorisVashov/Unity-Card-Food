﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour 
{
	bool isTouched = false;

	public int CardId;

	public GameObject backSide;
	public GameObject fronSide;
	public GameObject foodImageGameObject;

	Vector2 startPosition;

	Collider2D cardCollider;

	void Start() 
	{
		
	}

	public void InstalReferences(GameObject _foodImage, GameObject _frontSide, GameObject _backSide)
	{
		foodImageGameObject = _foodImage;
		fronSide = _frontSide;
		backSide = _backSide;

		cardCollider = GetComponent<Collider2D>();
	}

	public void DidTouch()
	{
		if (!isTouched)
		{
			ShowCard();
			isTouched = true;
		}
	}

	private void ShowCard()
	{
		backSide.SetActive(false);
		fronSide.SetActive(true);
		foodImageGameObject.SetActive(true);
	}

	public void ResetCard()
	{
		backSide.SetActive(true);
		fronSide.SetActive(false);
		foodImageGameObject.SetActive(false);

		// if (cardCollider != null)
		// {
			cardCollider.enabled = false;
		// }

		isTouched = false;
	}

	public void MoveCardToPosition(Vector2 targetPosition, bool isDealing)
	{	
		Vector2 direction = targetPosition - (Vector2)this.transform.position;
		direction.Normalize();

		startPosition = this.transform.position;

		float timeStartedLerping = Time.time;

		StartCoroutine(MoveToPosition(direction, targetPosition, timeStartedLerping, isDealing));
	}

	IEnumerator MoveToPosition(Vector2 direction, Vector2 targetPosition, float timeStartedLerping, bool isDealing)
	{
		float distance = Vector2.Distance(this.transform.position, targetPosition);

		float percentageComplete;

		float distCovered;

		while (true)
		{
			distCovered = (Time.time - timeStartedLerping) * GameRules.DealSpeed;
			percentageComplete = distCovered / distance;

			if (Vector2.Distance(this.transform.position, targetPosition) < 0.0001)
			{
				// Debug.Log("End of coroutine");
				if (isDealing)
				{
					cardCollider.enabled = true;
				}

				yield break;
			}

			this.transform.position = Vector2.Lerp(startPosition, targetPosition, percentageComplete);
			
			yield return null;
		}
	}

	public bool Equals(Card otherCard)
	{
		if (this.CardId == (otherCard as Card).CardId)
			return true;
		else
			return false;
	}

}