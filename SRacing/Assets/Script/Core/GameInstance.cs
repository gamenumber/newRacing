using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
	public static GameInstance instance;
	public float Speed;
	public float GamePlayTime = 0f;
	public int Coin = 0;
	public int Score = 0;
	public int RabCount = 0;
	public int EnemyRabCount = 0;
	public List<Part> partsList = new List<Part>();
	public Dictionary<string, bool> purchasedItems = new Dictionary<string, bool>();

	// purchasedTextures ����Ʈ ����
	public List<Texture2D> purchasedTextures = new List<Texture2D>();

	public List<Texture2D> selectedTextures = new List<Texture2D>();

	// �ε��� �ؽ�ó�� ��� ���
	private string[] texturePathsToLoad = { "DesertWheel", "MountainWheel", "DownTownWheel", "SixEngine", "EightEngine" };

	private void Start()
	{
		// ��� �ؽ�ó �ε�
		foreach (string texturePath in texturePathsToLoad)
		{
			LoadTexture(texturePath);
		}
	}
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void AddPart(Part part)
	{
		partsList.Add(part);
	}

	public void PurchaseItem(string itemName)
	{
		if (!purchasedItems.ContainsKey(itemName))
		{
			purchasedItems[itemName] = true;
			PlayerPrefs.SetInt(itemName, 1);
			PlayerPrefs.Save();
		}
	}


	public bool IsItemPurchased(string itemName)
	{
		if (purchasedItems.ContainsKey(itemName))
		{
			return purchasedItems[itemName];
		}
		else
		{
			int purchased = PlayerPrefs.GetInt(itemName, 0);
			bool isPurchased = purchased == 1;
			purchasedItems[itemName] = isPurchased;
			return isPurchased;
		}
	}

	// �ؽ�ó �ε� �� ����Ʈ�� �߰��ϴ� �޼ҵ�
	public void LoadTexture(string texturePath)
	{
		// Resources �������� �ؽ�ó �ε�
		Texture2D loadedTexture = Resources.Load<Texture2D>(texturePath);

		if (loadedTexture != null)
		{
			// �ε�� �ؽ�ó�� purchasedTextures ����Ʈ�� �߰�
			purchasedTextures.Add(loadedTexture);
			Debug.Log("Texture loaded successfully: " + loadedTexture.name);
		}
		else
		{
			Debug.LogError("Failed to load texture from path: " + texturePath);
		}
	}
}


