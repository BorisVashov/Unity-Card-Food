using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateDirector : MonoBehaviour 
{
	public GameObject ratingPage;
	void Start () 
	{
		
	}

	private void RateAppShow()
	{
		ratingPage.SetActive(true);
	}

	public void RateButtonPressed()
	{
		if (CheckNetworkAvailability())
		{
			Application.OpenURL(GetRateURL());
		}
	}

	private string GetRateURL()
	{
		string appID = "com.miniclip.plagueinc"; // GooglePlay, Amazon, Samsung

		string rateURL = "https://play.google.com/store/apps/details?id=" + appID; // GooglePlay

		return rateURL;
	}

	private bool CheckNetworkAvailability()
	{
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
