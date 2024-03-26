using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : BaseManager
{
	public Button[] itemButtons;
	private Button selectedButton;
	public Button purchaseButton;
	public GameObject ShopPage;
	public bool IsFree = false;
	public Dictionary<string, int> itemPrices = new Dictionary<string, int>();
	public CarMoveSystem PlayerCarMoveSystem;

	public List<RawImage> itemImages = new List<RawImage>();
	public List<Texture2D> purchasedTextures = new List<Texture2D>();

	void Start()
	{
		InitializeItemPrices();
		LoadPurchasedItems(); // ������ ������ �ҷ�����
		foreach (Button btn in itemButtons)
		{
			btn.onClick.AddListener(() => OnItemButtonClicked(btn));
			btn.GetComponent<Outline>().enabled = false;
		}
		purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
	}
	void LoadPurchasedItems()
	{
		foreach (var itemName in itemPrices.Keys)
		{
			if (GameInstance.instance.IsItemPurchased(itemName))
			{
				Texture2D partTexture = GetPartTexture(itemName);
				if (partTexture != null)
				{
					purchasedTextures.Add(partTexture);
					LoadItemTexture(itemName, partTexture);
				}
			}
		}
	}
	void LoadItemTexture(string itemName, Texture2D texture)
	{
		foreach (RawImage image in itemImages)
		{
			if (image.texture == null)
			{
				image.texture = texture;
				// �ؽ�ó �ε� �� PlayerPrefs�� ���� ���� ���������� ����
				PlayerPrefs.SetString("SelectedTexture", texture.name);
				PlayerPrefs.Save();
				break;
			}
		}
	}

	void PurchasePart(string partName)
	{
		Texture2D partTexture = GetPartTexture(partName);
		if (partTexture != null)
		{
			if (!purchasedTextures.Contains(partTexture)) // �ߺ� �߰� ����
			{
				purchasedTextures.Add(partTexture);
			
			}
			GameInstance.instance.PurchaseItem(partName); // ���� ���� ����
			AddPurchasedItemTexture(partTexture); // ���ο� �������� �ؽ�ó �߰�
		}
	}


	void InitializeItemPrices()
	{
		itemPrices.Add("DesertWheel", 10000000);
		itemPrices.Add("DownTownWheel", 10000000);
		itemPrices.Add("MountainWheel", 10000000);
		itemPrices.Add("SixEngine", 10000000);
		itemPrices.Add("EightEngine", 10000000);
	}

	void OnItemButtonClicked(Button button)
	{
		if (selectedButton != null)
		{
			selectedButton.GetComponent<Outline>().enabled = false;
		}
		selectedButton = button;
		selectedButton.GetComponent<Outline>().enabled = true;
	}


	Texture2D GetPartTexture(string partName)
	{
		foreach (Texture2D texture in purchasedTextures)
		{
			if (texture.name == partName)
			{
				return texture;
			}
		}
		return null;
	}

	void OnPurchaseButtonClicked()
	{
		if (selectedButton != null)
		{
			int itemPrice = GetItemPrice(selectedButton.gameObject.name);

			if (IsFree || GameInstance.instance.Coin >= itemPrice)
			{
				if (!IsFree)
				{
					GameInstance.instance.Coin -= itemPrice;
				}

				PurchasePart(selectedButton.gameObject.name);
				OnGetPart();
				selectedButton.GetComponent<Outline>().enabled = false;
				selectedButton = null;
			}
			else
			{
				Debug.Log("���� �����մϴ�.");
			}
		}
	}

	void OnGetPart()
	{
		string itemName = selectedButton.gameObject.name;
		foreach (Part part in GameManager.Instance.PartManager.Parts)
		{
			if (part.Prefab.name == itemName)
			{
				BasePart basePartScript = part.Prefab.GetComponent<BasePart>();
				basePartScript.OnGetPart(PlayerCarMoveSystem);
				break;
			}
		}
	}

	int GetItemPrice(string itemName)
	{
		if (itemPrices.ContainsKey(itemName))
		{
			return itemPrices[itemName];
		}
		else
		{
			Debug.LogError("�ش� ��ǰ�� ������ ã�� �� �����ϴ�: " + itemName);
			return 0;
		}
	}

	void AddPurchasedItemTexture(Texture2D newItemTexture)
	{
		// ���� ������ �������� �ؽ�ó�� purchasedTextures ����Ʈ�� �߰�
		purchasedTextures.Add(newItemTexture);

		// UI ������Ʈ�� ���� �ֱٿ� ������ �������� �ؽ�ó�� �߰�
		// itemImages ����Ʈ�� �� ������ �ִ��� Ȯ��
		bool itemAdded = false;
		foreach (RawImage image in itemImages)
		{
			if (image.texture == null)
			{
				image.texture = newItemTexture;
				itemAdded = true;
				break;
			}
		}

		// ��� itemImages ������ ���ִ� ���, ���ο� �������� ����Ʈ�� �߰����� ����
		// �ʿ��ϴٸ� ���⿡�� ����Ʈ ũ�⸦ �����ϰų�, ������ �������� �����ϴ� ������ �߰��� �� �ֽ��ϴ�.
		if (!itemAdded)
		{
			Debug.Log("UI�� �߰��� ������ �����ϴ�. UI�� Ȯ���ϰų�, ���� �������� �������ּ���.");
		}

		// ���� �߰��� �κ�: ���õ� �ؽ�ó�� PlayerPrefs�� ���� ���� ���������� ����
		PlayerPrefs.SetString("SelectedTexture", newItemTexture.name);
		PlayerPrefs.Save();
	}
	public void GoingShop()
	{
		ShopPage.SetActive(true);
		Time.timeScale = 0f;
	}

	public void ExitShop()
	{
		IsFree = false;
		ShopPage.SetActive(false);
		Time.timeScale = 1f;
	}
}