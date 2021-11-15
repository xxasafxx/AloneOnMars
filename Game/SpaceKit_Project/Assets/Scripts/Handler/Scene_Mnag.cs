using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Mnag : MonoBehaviour
{
	public void StartRestartGame()
	{
		SceneManager.LoadScene(1);
		Time.timeScale = 1;
	}
	public void OpenMainScene()
	{
		SceneManager.LoadScene(0);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
