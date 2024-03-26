using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixEngine : BasePart
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
		car = CarMoveSystem;
		car.Speed += 2;
		_particleSystem.Play();
	}
}
