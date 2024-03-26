using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumTypes
{
	public enum ItemName
	{
		Boost, Booost, smallgold, middlegold, biggold, shop
	}

	public enum PartName
	{
		DesertWheel, MountainWheel, DownTownWheel, SixEngine, EightEngine
	}
}

[System.Serializable]
public class Item
{
	public EnumTypes.ItemName Name;
	public GameObject Prefab;
}

public class BaseItem : MonoBehaviour
{
	public virtual void OnGetItem(PlayerController player) { }
}

public class ItemManager : BaseManager
{
	public List<Item> Items = new List<Item>();
	public List<Transform> WayPoints = new List<Transform>();
	public List<GameObject> CurrentSpawnItems = new List<GameObject>();

	public int maxSpawnCount = 3; // 한 지점에서 소환될 최대 아이템 수
	public float spawnInterval = 30f; // 아이템 생성 간격 (초)

	private void Start()
	{
		WayPoints = GameManager.Instance.Spawner.waypoints;
		InvokeRepeating("SpawnItemsInTrack", spawnInterval, spawnInterval);
	}

	public void SpawnItem(GameObject itemPrefab, Vector3 position)
	{
		Instantiate(itemPrefab, new Vector3(position.x, 0, position.z), Quaternion.identity);
	}
	private void SpawnItemsInTrack()
	{
		foreach (Transform waypoint in WayPoints)
		{
			if (Random.Range(0, 5) == 0)
			{
				int spawnCount = Random.Range(1, maxSpawnCount + 1); // 랜덤한 수의 아이템 생성
				for (int k = 0; k < spawnCount; k++)
				{
					int spawnIndex = Random.Range(0, Items.Count);
					Vector3 spawnPosition = new Vector3(waypoint.position.x + Random.Range(-1, 2) * 3f, 0, waypoint.position.z);
					spawnPosition = SetGroundPos(spawnPosition); // SetGroundPos 호출하여 지형 위에 위치하도록 수정
					GameObject instance = Instantiate(Items[spawnIndex].Prefab, spawnPosition, Quaternion.identity);
					CurrentSpawnItems.Add(instance);
				}
			}
		}
	}

	private Vector3 SetGroundPos(Vector3 t)
	{
		t += new Vector3(0, 100, 0);
		RaycastHit hit;
		if (Physics.Raycast(t, Vector3.down, out hit, 200, ~LayerMask.GetMask("Item")))
		{
			t = hit.point;
		}

		return t;
	}
}



