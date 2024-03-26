using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
	public bool isPaused = false;
	public GameObject GameStopPage;

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.F5))
		{
			PauseGame();
		}

		// ������ �Ͻ����� ���°� �ƴ� ���� GamePlayTime�� ������Ʈ�մϴ�.
		if (!isPaused)
		{
			GameInstance.instance.GamePlayTime += Time.deltaTime;
		}
	}

	public void PauseGame()
	{
		GameStop();
		isPaused = true;
		GameStopPage.gameObject.SetActive(true);
	}

	public void ResumeGame()
	{
		GameRestart();
		isPaused = false;
		GameStopPage.gameObject.SetActive(false);
	}

	public void GameStop()
	{
		Time.timeScale = 0;
	}

	public void GameRestart()
	{
		Time.timeScale = 1;
	}

	public void GoingMain()
	{
		SceneManager.LoadScene("MainMenu");
		GameInstance.instance = null;
		GameRestart();
	}



}