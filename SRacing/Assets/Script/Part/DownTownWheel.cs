using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownTownWheel : BasePart
{
	private ParticleSystem _particleSystem;
	public CarMoveSystem CarMoveSystem;
	public bool Isbuy = false;

	private void Awake()
	{
		_particleSystem = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		if (Isbuy)
		{
			if (CarMoveSystem.IsMoving())
			{
				if (!_particleSystem.isPlaying)
				{
					_particleSystem.Play();
				}
			}
			else
			{
				if (_particleSystem.isPlaying)
				{
					_particleSystem.Stop();
				}
			}
		}
	}

	public override void OnGetPart(CarMoveSystem car)
	{
		Isbuy = true;
		base.OnGetPart(car);
		this.CarMoveSystem = car;
		Debug.Log("µµ½Ã ¹ÙÄû");
	}
}

