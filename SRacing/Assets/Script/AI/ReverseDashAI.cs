using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseDashAI : BaseCar
{

	public float Motor = 1f;
	
	public override void Movement()
	{
		if (TargetPoint == null) TargetPoint = WayPoints.GetChild(WayIndex);
		if (Vector3.Distance(TargetPoint.position, transform.position) <= 10 && WayPoints.childCount > WayIndex + 1)
		{

			WayIndex++;
			if (WayPoints.childCount == WayIndex)
			{
				WayIndex = 0;
			}
			TargetPoint = WayPoints.GetChild(WayIndex);
		}

		Vector3 waypointRelativeDistance = transform.InverseTransformPoint(TargetPoint.position);
		waypointRelativeDistance /= waypointRelativeDistance.magnitude;
		steering = (waypointRelativeDistance.x / waypointRelativeDistance.magnitude) ;
		motor = Motor;
		base.Movement();
	}
}