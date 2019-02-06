using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LanguageDirector : MonoBehaviour 
{
	Dictionary<string, Dictionary<string, string>> languageDict;

	Dictionary<string, string> engDict;
	Dictionary<string, string> ruDict;

	public TextMeshProUGUI playButtonText;
	public TextMeshProUGUI scoreLabelText;
	public TextMeshProUGUI labelPauseText;
	public TextMeshProUGUI policyButtonText;
	public TextMeshProUGUI resetScoreButtonText;
	public TextMeshProUGUI creatorsButtonText;

	public TextMeshProUGUI resetScoreText;
	public TextMeshProUGUI yesLabel;
	public TextMeshProUGUI noLabel;


	public Image langButtonImage;

	public string currentLanguage;
	Dictionary<string, string> currentDict;

	private Sprite engLangSprite;
	private Sprite ruLangSprite;

	void Awake () 
	{
		engDict = new Dictionary<string, string>()
		{
			{"play", "PLAY"},
			{"best score", "best\nSCORE"},
			{"pause", "PAUSE"},
			{"policyBtn", "PRIVACY\nPOLICY"},
			{"resetBtn", "RESET\nSCORE"},
			{"creatorsBtn", "CREATORS"},
			{"resetScoreText", "Do you want to reset score?"},
			{"yes", "YES"},
			{"no", "NO"}
		};

		ruDict = new Dictionary<string, string>()
		{
			{"play", "ИГРАТЬ"},
			{"best score", "лучший\nСЧЕТ"},
			{"pause", "ПАУЗА"},
			{"policyBtn", "ПОЛИТИКА\nПРИВАТНОСТИ"},
			{"resetBtn", "СБРОСИТЬ\nСЧЕТ"},
			{"creatorsBtn", "СОЗДАТЕЛИ"},
			{"resetScoreText", "Вы хотите сбросить счет?"},
			{"yes", "ДА"},
			{"no", "НЕТ"}
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

		engLangSprite = Resources.Load<Sprite>("UISprites/btn_eng");
		ruLangSprite = Resources.Load<Sprite>("UISprites/btn_ru");

		SetSpriteToLanguageButton();

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

		currentDict.TryGetValue("pause", out tempString);
		labelPauseText.text = tempString;

		currentDict.TryGetValue("policyBtn", out tempString);
		policyButtonText.text = tempString;
		
		currentDict.TryGetValue("resetBtn", out tempString);
		resetScoreButtonText.text = tempString;
		
		currentDict.TryGetValue("creatorsBtn", out tempString);
		creatorsButtonText.text = tempString;

		currentDict.TryGetValue("resetScoreText", out tempString);
		resetScoreText.text = tempString;

		currentDict.TryGetValue("yes", out tempString);
		yesLabel.text = tempString;

		currentDict.TryGetValue("no", out tempString);
		noLabel.text = tempString;
	}

	private void SetSpriteToLanguageButton()
	{
		switch (currentLanguage)
		{
			case "eng":
				langButtonImage.sprite = engLangSprite;
				break;
			
			case "ru":
				langButtonImage.sprite = ruLangSprite;
				break;
			
		}
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

		SetSpriteToLanguageButton();

		InitializeText();

		PlayerPrefs.SetString("language", currentLanguage);
		PlayerPrefs.Save();
	}
}
