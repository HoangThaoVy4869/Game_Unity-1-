using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs 
{
	public static int bestScore
	{
		get => PlayerPrefs.GetInt(GameScore.BEST_SCORE, 0);

		set
		{
			int curScore = PlayerPrefs.GetInt(GameScore.BEST_SCORE);

			if(value > curScore)
			{
				PlayerPrefs.SetInt(GameScore.BEST_SCORE,value);
			}
		}
	} 
}
