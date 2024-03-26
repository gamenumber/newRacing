using Cinemachine;
using UnityEngine;

public class BoostItem : BaseItem
{
	public float BoostSpeed = 30f;
	private CinemachineVirtualCamera playerCamera;
	private Vector3 forwardDirection; // ī�޶� �ٶ󺸴� ������ �����ϱ� ���� ����
	public Rigidbody rb;

	public override void OnGetItem(PlayerController player)
	{
		base.OnGetItem(player);
		rb.velocity += forwardDirection * BoostSpeed;
		SoundManager.instance.PlaySFX("Dash");
	}

	void Start()
	{
		rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
		playerCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
	}

	void FixedUpdate()
	{
		// ī�޶� �ٶ󺸴� ������ ����
		forwardDirection = playerCamera.transform.forward;
		// y ���� 0���� ����
		forwardDirection.y = 0f;
		// ������ ����ȭ�Ͽ� ���̰� 1�� �ǵ��� ��
		forwardDirection.Normalize();
	}
}
