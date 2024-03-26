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
		LoadPurchasedItems(); // 구매한 아이템 불러오기
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
				// 텍스처 로드 후 PlayerPrefs를 통해 다음 스테이지로 전달
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
			if (!purchasedTextures.Contains(partTexture)) // 중복 추가 방지
			{
				purchasedTextures.Add(partTexture);
			
			}
			GameInstance.instance.PurchaseItem(partName); // 구매 정보 저장
			AddPurchasedItemTexture(partTexture); // 새로운 아이템의 텍스처 추가
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
				Debug.Log("돈이 부족합니다.");
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
			Debug.LogError("해당 상품의 가격을 찾을 수 없습니다: " + itemName);
			return 0;
		}
	}

	void AddPurchasedItemTexture(Texture2D newItemTexture)
	{
		// 새로 구매한 아이템의 텍스처를 purchasedTextures 리스트에 추가
		purchasedTextures.Add(newItemTexture);

		// UI 업데이트를 위해 최근에 구매한 아이템의 텍스처만 추가
		// itemImages 리스트에 빈 슬롯이 있는지 확인
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

		// 모든 itemImages 슬롯이 차있는 경우, 새로운 아이템을 리스트에 추가하지 않음
		// 필요하다면 여기에서 리스트 크기를 조정하거나, 오래된 아이템을 제거하는 로직을 추가할 수 있습니다.
		if (!itemAdded)
		{
			Debug.Log("UI에 추가할 공간이 없습니다. UI를 확장하거나, 기존 아이템을 제거해주세요.");
		}

		// 새로 추가된 부분: 선택된 텍스처를 PlayerPrefs를 통해 다음 스테이지로 전달
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