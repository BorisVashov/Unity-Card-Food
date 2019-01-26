using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour 
{
	RaycastHit2D[] hits = new RaycastHit2D[3];

	Vector3 startVector;

	Camera mainCamera;
	CardDealer cardDealer;

	void Start () 
	{
		mainCamera = Camera.main;
		cardDealer = GameObject.Find("CardDealer").GetComponent<CardDealer>();
	}
	
	void Update () 
	{
		

		#if UNITY_EDITOR

		if(Input.GetMouseButtonDown(0))
		{
			startVector = mainCamera.ScreenToWorldPoint(Input.mousePosition);

			Vector3 endVector = new Vector3(startVector.x, startVector.y, 20);
			Debug.DrawLine(startVector, endVector, Color.red, 5f);

			int countHits = Physics2D.RaycastNonAlloc(startVector, Vector2.zero, hits);//  (ray, hits, 20);

			for(int index = 0; index < countHits; index++)
			{
				if (hits[index].collider.tag == "Card")
				{
					Card card = hits[index].collider.gameObject.GetComponent<Card>();

					if (cardDealer.cardFirst == null)
					{
						cardDealer.cardFirst = card.DidTouch();
					}
					else if (cardDealer.cardSecond == null)
					{
						cardDealer.cardSecond = card.DidTouch();
					}

					break;
				}
				// {
				// 	Debug.Log("NO");
				// }
			}
		}
		#endif
		
		#if UNITY_ANDROID 

		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			startVector = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);

			Vector3 endVector = new Vector3(startVector.x, startVector.y, 20);
			Debug.DrawLine(startVector, endVector, Color.red, 5f);

			int countHits = Physics2D.RaycastNonAlloc(startVector, Vector2.zero, hits);//  (ray, hits, 20);

			for(int index = 0; index < countHits; index++)
			{
				if (hits[index].collider.tag == "Card")
				{
					Card card = hits[index].collider.gameObject.GetComponent<Card>();

					if (cardDealer.cardFirst == null)
					{
						cardDealer.cardFirst = card.DidTouch();
					}
					else if (cardDealer.cardSecond == null)
					{
						cardDealer.cardSecond = card.DidTouch();
					}

					break;
				}
			}
		}

		#endif

	}
}
