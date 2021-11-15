using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
	public int Coin;
	public int Wave = 1;
	public float CountDown = 0;

	bool Ispenal = false;

	[Header("Text")]
	[SerializeField] private GameObject CoinText;
	[SerializeField] private GameObject WaveText;
	[SerializeField] private GameObject CountDownText;

	[Header("Panel")]
	public GameObject NextWavePanel;

	[SerializeField] private GameObject GamePanel;
	[SerializeField] private GameObject GameOverPanel;
	[SerializeField] private GameObject GamePausePanel;

	private void Start()
	{
		Coin = 6500;
	}

	private void Update()
	{
		ShowUI();
		PauseGame();
	}


	void ShowUI()
	{
		CoinText.GetComponent<Text>().text = Coin + "$";
		WaveText.GetComponent<Text>().text = "Wave " + Wave;

		CountDownText.GetComponent<Text>().text = "WAVE " + Wave + " CLEARD " + " Next Wave Start  " + ((int)CountDown).ToString();
	}

	public void AddPoints(int coinValue) // called from enemy take damage
	{
		Coin += coinValue;
	}

	public void ChackGameOverUI()
	{
		GamePanel.SetActive(false);
		Cursor.lockState = CursorLockMode.None;
		GameOverPanel.SetActive(true);
	}

	public void PauseGame()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !Ispenal)
		{
			Ispenal = true;
			Time.timeScale = 0;
			Cursor.lockState = CursorLockMode.None;
			GamePausePanel.SetActive(true);
		}
		
	}

	public void ResumeGame()
	{
		Ispenal = false;
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Locked;
		GamePausePanel.SetActive(false);
	}
}
