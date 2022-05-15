using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float spawnTime;
	public Birds[] birdPrefabs;
	public int timeLimit;

	int m_curTimelimit;
	int m_birdKilled;

	bool m_isGameOver;

	public bool IsGameOver { get => m_isGameOver; set => m_isGameOver = value; }
	public int BirdKilled { get => m_birdKilled; set => m_birdKilled = value; }

	public override void Awake()
	{
		MakeSingleton(false);

		m_curTimelimit = timeLimit;

		//PlayerPrefs.DeleteAll();
	}
	public override void Start()
	{
		GameGunManager.Ins.ShowGameGun(false);
		GameGunManager.Ins.UpdateKillerCouting(m_birdKilled);
	}

	public void PlayGame()
	{
		StartCoroutine(GameSpawn());

		StartCoroutine(TimeCountDown());

		GameGunManager.Ins.ShowGameGun(true);
	}

	IEnumerator GameSpawn()
	{
		while (!IsGameOver)
		{
			SpawnBird();
			yield return new WaitForSeconds(spawnTime);
		}
	}
	IEnumerator TimeCountDown()
	{
		while(m_curTimelimit > 0)
		{
			yield return new WaitForSeconds(1f);

			m_curTimelimit--;

			if(m_curTimelimit <= 0)
			{
				m_isGameOver = true;
				if(m_birdKilled > Prefs.bestScore)
				{
					GameGunManager.Ins.gameDialog.UpdateD("NEW BEST", "BEST SCORE: x" + m_birdKilled);
				}
				else
				{
					GameGunManager.Ins.gameDialog.UpdateD("YOUR BEST", "BEST SCORE: x" + m_birdKilled);
				}

				Prefs.bestScore = m_birdKilled;

				GameGunManager.Ins.gameDialog.Show(true);
				GameGunManager.Ins.CurDialog = GameGunManager.Ins.gameDialog;

			}
			GameGunManager.Ins.UpdateTimer(IntToTime(m_curTimelimit));
		}
	}
	void SpawnBird()
	{
		Vector3 spawnPos = Vector3.zero;

		float randCheck = Random.Range(0f, 1f);

		if(randCheck >= 0.5f)
		{
			spawnPos = new Vector3(12, Random.Range(-2f, 4f),0);
		}
		else
		{
			spawnPos = new Vector3(-12, Random.Range(-2f, 4f),0);
		}
		if (birdPrefabs != null && birdPrefabs.Length > 0)
		{
			int randIdx = Random.Range(0, birdPrefabs.Length);

			if(birdPrefabs[randIdx] != null)
			{
				Birds birdClone = Instantiate(birdPrefabs[randIdx], spawnPos, Quaternion.identity);
			}
		}
	}
	string IntToTime(int time)
	{
		float minutes = Mathf.Floor(time / 60);
		float seconds = Mathf.RoundToInt(time % 60);
		return minutes.ToString("00") + ":" + seconds.ToString("00");
	}



}
