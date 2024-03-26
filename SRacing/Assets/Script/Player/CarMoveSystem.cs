using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class WheelInfo
{
	public WheelCollider L_Wheel;
	public WheelCollider R_Wheel;

	public bool Motor;
	public bool Steering;
}

public class CarMoveSystem : MonoBehaviour
{
	public WheelInfo[] WheelInfo;
	public float Speed = 10;
	public float MaxMotor;
	public float MaxSteer;
	public float BreakForce;
	public float minmovement = 0.1f;
	public Transform center;
	public Rigidbody rb;
	public float GameStartDelayTime = 3.6f;

	private bool bStageStart = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.centerOfMass = center.localPosition;

		StartCoroutine(StageStart());
	}
	public bool IsMoving()
	{
		return rb.velocity.magnitude > minmovement;
	}

	IEnumerator StageStart()
	{
		yield return new WaitForSeconds(GameStartDelayTime);
		bStageStart = true;
	}

	public void MoveWheel(float moterTorque, float steer, bool bIsBreak)
	{
		if (bStageStart)
		{
			moterTorque *= MaxMotor * Speed;
			steer *= MaxSteer;

			foreach (var wheel in WheelInfo)
			{
				if (wheel.Motor)
				{
					wheel.L_Wheel.motorTorque = moterTorque;
					wheel.R_Wheel.motorTorque = moterTorque;
				}

				if (wheel.Steering)
				{
					wheel.L_Wheel.steerAngle = steer;
					wheel.R_Wheel.steerAngle = steer;
				}

				float isbreak = (bIsBreak ? 1 : 0);

				wheel.L_Wheel.brakeTorque = BreakForce * isbreak;
				wheel.R_Wheel.brakeTorque = BreakForce * isbreak;
			}
		}
		
	}
}
