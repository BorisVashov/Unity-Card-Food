using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{	
	private static PlayerState _playerOne;

	private static PlayerState _playerTwo;

	public PlayerState PlayerOne
	{
		get 
		{
			if (_playerOne == null)
			{
				_playerOne = new PlayerState();
			}

			return _playerOne;
		}
	}

	public PlayerState PlayerTwo
	{
		get 
		{
			if (_playerTwo == null)
			{
				_playerTwo = new PlayerState();
			}

			return _playerTwo;
		}
	}

	public Card cardFirst;
	public Card cardSecond;

}
