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
		Debug.Log("OnGetPart 호출됨");
		Debug.Log("CarMoveSystem: " + (CarMoveSystem == null ? "null" : "not null"));
		Debug.Log("car 매개변수: " + (car == null ? "null" : "not null"));
		Debug.Log("_particleSystem: " + (_particleSystem == null ? "null" : "not null"));

		car = CarMoveSystem;
		if (car != null)
		{
			car.Speed += 4;
			Debug.Log("속도 증가: " + car.Speed);
		}
		else
		{
			Debug.Log("CarMoveSystem이 null입니다.");
		}

		if (_particleSystem != null)
		{
			_particleSystem.Play();
		}
		else
		{
			Debug.Log("_particleSystem이 null입니다.");
		}
	}

}
