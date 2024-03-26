using Cinemachine;
using UnityEngine;

public class BoostItem : BaseItem
{
	public float BoostSpeed = 30f;
	private CinemachineVirtualCamera playerCamera;
	private Vector3 forwardDirection; // 카메라가 바라보는 방향을 저장하기 위한 변수
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
		// 카메라가 바라보는 방향을 얻어옴
		forwardDirection = playerCamera.transform.forward;
		// y 값을 0으로 고정
		forwardDirection.y = 0f;
		// 방향을 정규화하여 길이가 1이 되도록 함
		forwardDirection.Normalize();
	}
}
