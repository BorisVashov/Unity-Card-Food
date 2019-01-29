using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageDirector : MonoBehaviour 
{
	Dictionary<string, Dictionary<string, string>> languageDict;

	Dictionary<string, string> engDict;
	Dictionary<string, string> ruDict;

	public TextMeshProUGUI playButtonText;
	public TextMeshProUGUI scoreLabelText;

	public string currentLanguage;
	Dictionary<string, string> currentDict;


	void Awake () 
	{
		engDict = new Dictionary<string, string>()
		{
			{"play", "PLAY"},
			{"best score", "best\nSCORE"}
		};

		ruDict = new Dictionary<string, string>()
		{
			{"play", "ИГРАТЬ"},
			{"best score", "лучший\nСЧЕТ"}
		};

		languageDict = new Dictionary<string, Dictionary<string, string>>()
		{
			{"eng", engDict},
			{"ru", ruDict}
		};
	}

	void Start()
	{
		currentLanguage = PlayerPrefs.GetString("language", "eng");

		InitializeText();
	}

	private void InitializeText()
	{	
		languageDict.TryGetValue(currentLanguage, out currentDict);

		string tempString;
		currentDict.TryGetValue("play", out tempString);

		playButtonText.text = tempString;

		currentDict.TryGetValue("best score", out tempString);
		scoreLabelText.text = tempString;
	}
	
	public void ChangeTextLanguage() 
	{
		switch(currentLanguage)
		{
			case "eng":
				currentLanguage = "ru";
				break;

			case "ru":
				currentLanguage = "eng";
				break;
		}

		InitializeText();

		PlayerPrefs.SetString("language", currentLanguage);
		PlayerPrefs.Save();
	}
}
