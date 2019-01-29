using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioDirector : MonoBehaviour 
{	
	private bool isMusicOn = true;

	private Sprite musicOnSprite;
	private Sprite musicOffSprite;

	public Image musicButtonImage;

	void Awake () 
	{
		musicOnSprite = Resources.Load<Sprite>("UISprites/btn_mus_on");
		musicOffSprite = Resources.Load<Sprite>("UISprites/btn_mus_off");
	}

	public void MusicChangeButton()
	{
		if (isMusicOn)
		{
			AudioListener.volume = 0;

			musicButtonImage.sprite = musicOffSprite;
			isMusicOn = false;
		}
		else
		{
			AudioListener.volume = 1;

			musicButtonImage.sprite = musicOnSprite;
			isMusicOn = true;
		}
	}
}
