using System;
using System.Collections;
using System.Collections.Generic; // List를 사용하기 위해 필요
using UnityEngine;
using UnityEngine.EventSystems;

public class AIController : MonoBehaviour
{
	private CarMoveSystem _carMoveSystem;
	public List<Transform> WayPoints; // 배열 대신 List 사용
	private Vector3 _targetPoint;
	private int _wayPointCount = 1;
	public float MoveSpeed = 5;

	private void Start()
	{
		WayPoints = new List<Transform>(GameManager.Instance.Spawner.waypoints); // List로 초기화
		_carMoveSystem = GetComponent<CarMoveSystem>();
	}

	private void FixedUpdate()
	{
		MoveAI();
	}

	void MoveAI()
	{
		FindNearWaypoint();

		Vector3 WaypointDistance = transform.InverseTransformPoint(_targetPoint);
		WaypointDistance = WaypointDistance.normalized;
		float steering = WaypointDistance.x;

		_carMoveSystem.MoveWheel(1, steering, false);
	}

	void FindNearWaypoint()
	{
		_targetPoint = WayPoints[_wayPointCount].position;
		if (Vector3.Distance(transform.position, _targetPoint) <= 3f)
		{
			if (WayPoints.Count - 1 > _wayPointCount)
			{
				_wayPointCount++;
			}
			else
			{
				_carMoveSystem.MoveWheel(0, 0, true);
				return;
			}
		}
	}

}