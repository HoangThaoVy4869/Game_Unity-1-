using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGunManager : Singleton<GameGunManager>
{
	public GameObject homeGun;
	public GameObject gameGun;

	public GameDialog gameDialog;
	public GameDialog pauseDialog;

	public Image fireRateFilled;
	public Text timerText;
	public Text killedCountingText;

	GameDialog m_curDialog;

	public GameDialog CurDialog { get => m_curDialog; set => m_curDialog = value; }

	public override void Awake()
	{
		MakeSingleton(false);
	}
	public void ShowGameGun(bool isShow)
	{
		if (gameGun)
		{
			gameGun.SetActive(isShow);
		}
		if (homeGun)
			homeGun.SetActive(!isShow);
	}
	public void UpdateTimer(string time)
	{
		if (timerText)
			timerText.text = time;
	}
	public void UpdateKillerCouting(int killed)
	{
		if (killedCountingText)
			killedCountingText.text = "x " + killed.ToString();
	}
	public void UpdateFireRate(float rate)
	{
		if (fireRateFilled)
			fireRateFilled.fillAmount = rate;
	}

	public void PauseGame()
	{
		Time.timeScale = 0f;

		if (pauseDialog)
		{
			pauseDialog.Show(true);
			pauseDialog.UpdateD("GAME PAUSE", "BEST KILLED: X" + Prefs.bestScore);

			m_curDialog = pauseDialog;
		}
	}
	public void ResumeGame()
	{
		Time.timeScale = 1f;
		if (m_curDialog)
			m_curDialog.Show(false);
	}

	public void Home()
	{
		ResumeGame();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Replay()
	{
		if (m_curDialog)
			m_curDialog.Show(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Exit()
	{
		ResumeGame();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		Application.Quit();
	}

}
