using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public ShopManager ShopManager;
	public ItemManager ItemManager;
	public PartManager PartManager;
	public AIEnemySpawnManager Spawner;
	public SoundManager SoundManager;
	public RankingManager RankingManager;

	public TextMeshProUGUI CountdownText;

	public GameObject F1Page;

	private IEnumerator CountDown()
	{
		SoundManager.StopAllSFX();
		SoundManager.PlaySFX("CountDown");

		CountdownText.text = "3";
		yield return new WaitForSeconds(1f);
		CountdownText.text = "2";
		yield return new WaitForSeconds(1f);
		CountdownText.text = "1";
		yield return new WaitForSeconds(1f);

		CountdownText.gameObject.SetActive(false);

		if (SceneManager.GetActiveScene().name == "Stage1")
		{
			SoundManager.PlayBGM("Bgm1");
		}
		else if (SceneManager.GetActiveScene().name == "Stage2")
		{
			SoundManager.PlayBGM("Bgm2");
		}
		else if (SceneManager.GetActiveScene().name == "Stage3")
		{
			SoundManager.PlayBGM("Bgm3");
		}
	}
	void Start()
	{
		StartCoroutine(CountDown());

		if (ShopManager)
			ShopManager.Init(this);
		if (ItemManager)
			ItemManager.Init(this);
		if (PartManager)
			PartManager.Init(this);
		if (Spawner) 
			Spawner.Init(this);
		if (SoundManager)		
			SoundManager.Init(this);
		if (RankingManager)
			RankingManager.Init(this);
	}

	public void InitnumberofLab()
	{
		GameInstance.instance.RabCount = 0;
		GameInstance.instance.EnemyRabCount = 0;
	}

	private void Awake()
	{	
		if (Instance == null)
		{
			Instance = this;
		}
		else
			Destroy(this.gameObject);
	}

	public void GameStart()
	{
		SceneManager.LoadScene("Stage1");
	}

	public void GoingNextStage()
	{
		Time.timeScale = 1f;
		string currentSceneName = SceneManager.GetActiveScene().name;

		switch (currentSceneName)
		{
			case "Stage1":
				SceneManager.LoadScene("Stage2");
				break;

			case "Stage2":
				SceneManager.LoadScene("Stage3");
				break;

			case "Stage3":
				SceneManager.LoadScene("Result");
				break;
		}

	}

	public void RestartStage()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene($"{SceneManager.GetActiveScene().name}");
	}

	public void StageEnd()
	{
		if (GameInstance.instance.GamePlayTime < 300f)
		{
			AddScore(100);
		

		}

		else if (GameInstance.instance.GamePlayTime < 320f)
		{
			AddScore(80);
			

		}

		else if (GameInstance.instance.GamePlayTime < 360f)
		{
			AddScore(40);
			

		}

		else if (GameInstance.instance.GamePlayTime < 400f)
		{
			AddScore(20);
			

		}

		else
		{
			AddScore(10);
			
		}

	}

	public void GameQuit()
	{
		Application.Quit();
	}
	public void GoingMain()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void AddScore(int score)
	{
		GameInstance.instance.Score += score;
	}
	public void AddCoin(int coin)
	{
		GameInstance.instance.Coin += coin;
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.F1))
		{
			F1Page.gameObject.SetActive(true);
		}


		if (Input.GetKeyUp(KeyCode.F2))
		{
			ShopManager.GoingShop();
			ShopManager.IsFree = true;
		}


		if (Input.GetKeyUp(KeyCode.F3))
		{
			RestartStage();
		}


		if (Input.GetKeyUp(KeyCode.F4))
		{
			string currentSceneName = SceneManager.GetActiveScene().name;

			switch (currentSceneName)
			{
				case "Stage1":
					SceneManager.LoadScene("Stage2");
					GameInstance.instance.GamePlayTime = 0f;
					break;

				case "Stage2":
					SceneManager.LoadScene("Stage3");
					GameInstance.instance.GamePlayTime = 0f;
					break;

				default:
					SceneManager.LoadScene("Stage1");
					GameInstance.instance.GamePlayTime = 0f;
					break;
			}

		}
	}

	public void BoostItem()
	{
		SpawnItemByName(EnumTypes.ItemName.Boost);
		F1Page.gameObject.SetActive(false);
	}

	public void BooostItem()
	{
		SpawnItemByName(EnumTypes.ItemName.Booost);
		F1Page.gameObject.SetActive(false);
	}

	public void ShopItem()
	{
		SpawnItemByName(EnumTypes.ItemName.shop);
		F1Page.gameObject.SetActive(false);
	}

	public void SmallGoldItem()
	{
		SpawnItemByName(EnumTypes.ItemName.smallgold);
		F1Page.gameObject.SetActive(false);
	}

	public void MiddleGoldItem()
	{
		SpawnItemByName(EnumTypes.ItemName.middlegold);
		F1Page.gameObject.SetActive(false);
	}

	public void BigGoldItem()
	{
		SpawnItemByName(EnumTypes.ItemName.biggold);
		F1Page.gameObject.SetActive(false);
	}

	private void SpawnItemByName(EnumTypes.ItemName itemName)
	{
		Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
		Item item = ItemManager.Items.Find(i => i.Name == itemName);
		if (item != null)
		{
			ItemManager.SpawnItem(item.Prefab, playerPosition);
		}
	}
}