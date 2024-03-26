using UnityEngine;

public class SixEngine : BasePart
{
	public ParticleSystem _particleSystem;
	public CarGroundEffectSystem _carGroundEffectSystem;

	private void Start()
	{
		// EightEngine ��ũ��Ʈ�� �÷��̾� ������Ʈ�� �߰��� �� Start �޼��尡 ȣ��ǹǷ�,
		// EightEngine ��ũ��Ʈ�� Start �޼��忡�� ��ƼŬ �ý����� ���������� ����
		if (_particleSystem == null)
		{
			_particleSystem = GetComponent<ParticleSystem>();
		}
	}

	public override void OnGetPart(CarMoveSystem car)
	{
		base.OnGetPart(car);

		// ��ƼŬ �ý��۰� CarGroundEffectSystem ����
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
