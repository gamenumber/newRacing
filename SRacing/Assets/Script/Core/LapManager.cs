using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class LapManager : MonoBehaviour
{
	public TextMeshProUGUI lapText; 
	public TextMeshProUGUI EnemylapText2;
	public int totalLaps = 5;
	private int currentLap;
	private int competitorLab;

	public GameObject WinUI;
	public GameObject LoseUI;

	private bool gameEnded = false; // 게임 종료 플래그 추가

	private void Update()
	{
		if (!gameEnded)
		{
			GameInstance.instance.RabCount = currentLap;
			GameInstance.instance.EnemyRabCount = competitorLab;
			lapText.text = $"Player : {currentLap} / {totalLaps}";
			EnemylapText2.text = $"Competitor : {competitorLab} / {totalLaps}";
			GameEnd();
		} 
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (gameEnded) return; 

		if (other.CompareTag("Player") && currentLap < totalLaps)
		{
			currentLap += 1;
		}

		if (other.CompareTag("Competitor") && competitorLab < totalLaps)
		{
			competitorLab += 1;
		}
	}

	public void GameEnd()
	{
		if (competitorLab >= totalLaps)
		{
			GameInstance.instance.GamePlayTime = 0;
			Time.timeScale = 0;
			LoseUI.SetActive(true);
			gameEnded = true; // 게임 종료 플래그 설정
		}
		else if (currentLap >= totalLaps)
		{
			GameInstance.instance.GamePlayTime = 0;
			Time.timeScale = 0;
			GameManager.Instance.StageEnd();
			WinUI.SetActive(true);
			gameEnded = true; // 게임 종료 플래그 설정
		}
	}

}
