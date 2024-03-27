using UnityEngine;

public class SixEngine : BasePart
{
	public ParticleSystem _particleSystem;
	public CarGroundEffectSystem _carGroundEffectSystem;
	public PlayerController _playerController;

	public override void OnGetPart(CarMoveSystem car)
	{
		_playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		_carGroundEffectSystem = GameObject.FindWithTag("Player").GetComponent<CarGroundEffectSystem>();
		if (_particleSystem == null)
		{
			_particleSystem = GetComponent<ParticleSystem>();
		}
		base.OnGetPart(car);
		_carGroundEffectSystem.baseSpeed += 2;
		GameInstance.instance.Speed = _carGroundEffectSystem.baseSpeed;
		_playerController.b_buySix = true;

	}
}
