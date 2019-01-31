using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdDirector : MonoBehaviour 
{	
	public GameObject mainMenu;
	public GameObject privacyPolicyMenu;

	[SerializeField] private bool isPolicyAccept;

	private bool isFirstAppRun = true;

	void Start () 
	{
		isFirstAppRun = bool.Parse(PlayerPrefs.GetString("isFirstAppRun", "true"));

		if (isFirstAppRun)
		{
			privacyPolicyMenu.SetActive(true);
		}
		else
		{
			mainMenu.SetActive(true);
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
