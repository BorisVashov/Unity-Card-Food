using System.Collections;
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

		startPosition = this.gameObject.transform.position;

		cardCollider = GetComponent<Collider2D>();
	}

	public Card DidTouch()
	{
		if (!isTouched)
		{
			ShowCard();
			isTouched = true;

			return this;
		}
		else
		{
			return null;
		}
	}

	public void ShowCard()
	{
		backSide.SetActive(false);
		fronSide.SetActive(true);
		foodImageGameObject.SetActive(true);
	}

	public void HideCard()
	{
		backSide.SetActive(true);
		fronSide.SetActive(false);
		foodImageGameObject.SetActive(false);

		isTouched = false;
	}

	public void ResetCard()
	{
		HideCard();

		cardCollider.enabled = false;
	}

	public void GetBackCardToDeck()
	{
		ResetCard();

		this.gameObject.transform.position = startPosition;

		this.gameObject.SetActive(false);
	}

	public void MoveCardToPosition(Vector2 targetPosition, bool isDealing)
	{	
		Vector2 direction = targetPosition - (Vector2)this.transform.position;
		direction.Normalize();

		// startPosition = this.transform.position;

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

	public void AddCardToScore()
	{
		StartCoroutine(AddCardToScoreCoroutine());
	}

	IEnumerator AddCardToScoreCoroutine()
	{
		yield return new WaitForSeconds(GameRules.TimeBeforeResetChoosenCards);

		this.ResetCard();
		this.gameObject.transform.position = startPosition;
	}

}
