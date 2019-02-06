using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class AdDirector : MonoBehaviour 
{	
	public GameObject mainMenu;
	public GameObject privacyPolicyMenu;

	[SerializeField] private bool isPolicyAccept;

	string appKey = "7661d9f139d4747ab483bba4d85acfe65db4adc22f13ef2e";

	private bool isFirstAppRun = true;

	private float timeBetweenAds = 60f * 3f;
	private float timeLastShowAds;

	void Start () 
	{
		timeLastShowAds = Time.time;

		isFirstAppRun = bool.Parse(PlayerPrefs.GetString("isFirstAppRun", "true"));

		if (isFirstAppRun)
		{
			privacyPolicyMenu.SetActive(true);
		}
		else
		{
			isPolicyAccept = bool.Parse(PlayerPrefs.GetString("policy", "false"));
			
			InitializeAds();
			mainMenu.SetActive(true);
		}
	}

	private void InitializeAds()
	{
		Appodeal.initialize(appKey, Appodeal.INTERSTITIAL, isPolicyAccept);
	}

	public void MaybeTryShowAd()
	{
		if (Time.time - timeLastShowAds > timeBetweenAds)
		{
			if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
			{
				Appodeal.show(Appodeal.INTERSTITIAL);

				timeLastShowAds = Time.time;
			}
		}
		
	}
	
	public void AcceptPolicyPressed()
	{
		isPolicyAccept = true;

		PlayerPrefs.SetString("policy", "true");
		PlayerPrefs.SetString("isFirstAppRun", "false");
		PlayerPrefs.Save();

		ActivateMenu();
	}

	public void DeclinePolicyPressed()
	{
		isPolicyAccept = false;

		PlayerPrefs.SetString("policy", "false");
		PlayerPrefs.SetString("isFirstAppRun", "false");
		PlayerPrefs.Save();

		ActivateMenu();
	}

	public void MoreInfoPrivacyPressed()
	{
		if (CheckNetworkAvailability())
		{
			Application.OpenURL(GetPrivacyPolicyURL());
		}
	}

	private void ActivateMenu()
	{
		mainMenu.SetActive(true);
		privacyPolicyMenu.SetActive(false);

		InitializeAds();
	}

	private string GetPrivacyPolicyURL()
	{
		string policyURL = "https://www.appodeal.com/home/privacy-policy/"; // Appodeal privacy policy

		return policyURL;
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
