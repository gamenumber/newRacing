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
	public Transform playerTransform; // �÷��̾��� Transform�� �Ҵ��� ����

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
			// �÷��̾�� ���� ����� waypoint ã��
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

			// ���� ����� waypoint���� �������� �̵��ϱ�
			if (closestWaypointIndex != -1)
			{
				// ���� �ε������� �������� �迭�� Ž��
				for (int i = closestWaypointIndex; i >= 0; i--)
				{
					Vector3 spawnPosition = waypoints[i].position + (Random.insideUnitSphere.normalized * spawnRadius);

					if (!usedPositions.Contains(spawnPosition))
					{
						usedPositions.Add(spawnPosition);
						GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
						Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

						// �� ���� �ϳ��� ���� �����ϵ��� ������ ��������
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
				// ��������Ʈ ���̿� ���� �׸�
				Gizmos.color = Color.red; // ���� ���� ����
				Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);

				// ��������Ʈ ��ġ�� ��ü�� �׸�
				Gizmos.color = Color.blue; // ��ü�� ���� ����
				Gizmos.DrawSphere(waypoints[i].position, 0.5f); // ������ ��������Ʈ���� ��ü�� �׸�
			}
			// ������ ��������Ʈ�� ��ü�� �׸��� �ڵ�
			Gizmos.DrawSphere(waypoints[waypoints.Count - 1].position, 0.5f);
		}
	}
}
