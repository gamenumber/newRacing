using UnityEngine;

public class SixEngine : BasePart
{
	public ParticleSystem _particleSystem;
	public CarGroundEffectSystem _carGroundEffectSystem;

	private void Start()
	{
		// EightEngine 스크립트가 플레이어 오브젝트에 추가될 때 Start 메서드가 호출되므로,
		// EightEngine 스크립트의 Start 메서드에서 파티클 시스템을 가져오도록 수정
		if (_particleSystem == null)
		{
			_particleSystem = GetComponent<ParticleSystem>();
		}
	}

	public override void OnGetPart(CarMoveSystem car)
	{
		base.OnGetPart(car);

		// 파티클 시스템과 CarGroundEffectSystem 설정
		if (_particleSystem != null)
		{
			_particleSystem.Play();
		}
		else
		{
			Debug.LogError("Particle system not found.");
		}

		if (_carGroundEffectSystem != null)
		{
			_carGroundEffectSystem.baseSpeed += 2;
			GameInstance.instance.Speed = _carGroundEffectSystem.baseSpeed;
		}
		else
		{
			Debug.LogError("Car ground effect system not found.");
		}
	}
}
