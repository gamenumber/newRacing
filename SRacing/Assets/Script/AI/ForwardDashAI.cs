using UnityEngine;

public class ForwardDashAI : BaseCar
{
	public float playerTargetDistance = 7f; // 플레이어를 향해 돌진하기 시작하는 거리
	public float Moter;

	public override void Movement()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (TargetPoint == null) TargetPoint = WayPoints.GetChild(WayIndex);
		if (Vector3.Distance(player.transform.position, transform.position) <= 150)
		{
			TargetPoint = player.transform;
		}
		else if (Vector3.Distance(TargetPoint.position, transform.position) <= 10)
		{
			Debug.Log(WayIndex);
			WayIndex++;

			if (WayPoints.childCount == WayIndex)
			{
				WayIndex = 0;
				TargetPoint = WayPoints.GetChild(0);
			}
			TargetPoint = WayPoints.GetChild(WayIndex);
		}
		Vector3 waypointRelativeDistance = transform.InverseTransformPoint(TargetPoint.position);
		waypointRelativeDistance /= waypointRelativeDistance.magnitude;
		steering = (waypointRelativeDistance.x / waypointRelativeDistance.magnitude);
		motor = Moter;
		base.Movement();
	}
}