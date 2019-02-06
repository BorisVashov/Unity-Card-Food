using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioDirector : MonoBehaviour 
{	
	private bool isMusicOn;

	private Sprite musicOnSprite;
	private Sprite musicOffSprite;

	public Image musicButtonImage;

	void Awake () 
	{
		musicOnSprite = Resources.Load<Sprite>("UISprites/btn_mus_on");
		musicOffSprite = Resources.Load<Sprite>("UISprites/btn_mus_off");
	}

	void Start()
	{
		isMusicOn = bool.Parse(PlayerPrefs.GetString("music", "true"));
		
		UpdateMusicStatus();
	}

	public void MusicChangeButton()
	{
		isMusicOn = !isMusicOn;

		UpdateMusicStatus();

		SaveMusicStatus();
	}

	private void UpdateMusicStatus()
	{
		if (isMusicOn)
		{
			musicButtonImage.sprite = musicOnSprite;
			AudioListener.volume = 1;
		}
		else
		{
			musicButtonImage.sprite = musicOffSprite;
			AudioListener.volume = 0;
		}
	}

	private void SaveMusicStatus()
	{
		PlayerPrefs.SetString("music", isMusicOn.ToString());
		PlayerPrefs.Save();
	}
}
