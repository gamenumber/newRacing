using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightEngine : BasePart
{
	public ParticleSystem _particleSystem;
	public CarMoveSystem CarMoveSystem;

	private void Awake()
	{
		_particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
	}

	public override void OnGetPart(CarMoveSystem car)
	{
		base.OnGetPart(car);
		Debug.Log("OnGetPart ȣ���");
		Debug.Log("CarMoveSystem: " + (CarMoveSystem == null ? "null" : "not null"));
		Debug.Log("car �Ű�����: " + (car == null ? "null" : "not null"));
		Debug.Log("_particleSystem: " + (_particleSystem == null ? "null" : "not null"));

		car = CarMoveSystem;
		if (car != null)
		{
			car.Speed += 4;
			Debug.Log("�ӵ� ����: " + car.Speed);
		}
		else
		{
			Debug.Log("CarMoveSystem�� null�Դϴ�.");
		}

		if (_particleSystem != null)
		{
			_particleSystem.Play();
		}
		else
		{
			Debug.Log("_particleSystem�� null�Դϴ�.");
		}
	}

}
