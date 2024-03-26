using UnityEngine;

public class Warning : MonoBehaviour
{
	public Transform player;
	public float orbitDegreesPerSec = 30f;
	public float orbitDistance = 5f;

	void Update()
	{
		OrbitAroundPlayer();
		LookAtClosestEnemy();
	}

	void OrbitAroundPlayer()
	{
		if (player != null)
		{
			transform.RotateAround(player.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
			Vector3 direction = (transform.position - player.position).normalized * orbitDistance;
			transform.position = player.position + direction;
		}
	}

	void LookAtClosestEnemy()
	{
		GameObject closestEnemy = FindClosestEnemy();
		if (closestEnemy != null)
		{
			transform.LookAt(closestEnemy.transform);
		}
	}

	GameObject FindClosestEnemy()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = player.position;
		foreach (GameObject enemy in enemies)
		{
			Vector3 directionToTarget = enemy.transform.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if (dSqrToTarget < closestDistanceSqr)
			{
				closestDistanceSqr = dSqrToTarget;
				closest = enemy;
			}
		}
		return closest;
	}
}
