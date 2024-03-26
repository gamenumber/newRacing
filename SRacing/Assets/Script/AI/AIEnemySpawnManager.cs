using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemySpawnManager : BaseManager
{
	public List<Transform> waypoints = new List<Transform>();
	public List<GameObject> enemyPrefabs;
	public float spawnInterval = 10f;
	public int maxEnemiesPerSpawn = 3;
	private List<Vector3> usedPositions = new List<Vector3>();
	public float spawnRadius = 5f;
	public Transform playerTransform; // 플레이어의 Transform을 할당할 변수

	void Start()
	{
		StartCoroutine(SpawnEnemiesAtIntervals());
	}

	IEnumerator SpawnEnemiesAtIntervals()
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnInterval);
			usedPositions.Clear();
			int enemiesToSpawn = Random.Range(1, maxEnemiesPerSpawn + 1);
			for (int i = 0; i < enemiesToSpawn; i++)
			{
				SpawnEnemyInReverseOrder();
			}
		}
	}

	void SpawnEnemyInReverseOrder()
	{
		if (waypoints.Count > 0 && playerTransform != null)
		{
			// 플레이어와 가장 가까운 waypoint 찾기
			int closestWaypointIndex = -1;
			float closestDistance = float.MaxValue;
			for (int i = 0; i < waypoints.Count; i++)
			{
				float distance = Vector3.Distance(playerTransform.position, waypoints[i].position);
				if (distance < closestDistance)
				{
					closestDistance = distance;
					closestWaypointIndex = i;
				}
			}

			// 가장 가까운 waypoint에서 역순으로 이동하기
			if (closestWaypointIndex != -1)
			{
				// 현재 인덱스에서 역순으로 배열을 탐색
				for (int i = closestWaypointIndex; i >= 0; i--)
				{
					Vector3 spawnPosition = waypoints[i].position + (Random.insideUnitSphere.normalized * spawnRadius);

					if (!usedPositions.Contains(spawnPosition))
					{
						usedPositions.Add(spawnPosition);
						GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
						Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

						// 한 번에 하나의 적만 스폰하도록 루프를 빠져나옴
						break;
					}
				}
			}
		}
	}

	void OnDrawGizmos()
	{
		if (waypoints.Count > 1)
		{
			for (int i = 0; i < waypoints.Count - 1; i++)
			{
				// 웨이포인트 사이에 선을 그림
				Gizmos.color = Color.red; // 선의 색상 지정
				Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);

				// 웨이포인트 위치에 구체를 그림
				Gizmos.color = Color.blue; // 구체의 색상 지정
				Gizmos.DrawSphere(waypoints[i].position, 0.5f); // 마지막 웨이포인트에도 구체를 그림
			}
			// 마지막 웨이포인트에 구체를 그리는 코드
			Gizmos.DrawSphere(waypoints[waypoints.Count - 1].position, 0.5f);
		}
	}
}
