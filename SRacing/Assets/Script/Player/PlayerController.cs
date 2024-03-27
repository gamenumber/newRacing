using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private CarMoveSystem _carMoveSystem;
	private float baseSpeed = 10f;

	public GameObject SixEngine;
	public GameObject EightEngine;

	public bool b_buySix;
	public bool b_buyEight;
	
	public GameObject impactEffectPrefab;
	void Start()
	{
		_carMoveSystem = GetComponent<CarMoveSystem>();
		 b_buyEight = GameInstance.instance.EightEngine;
		 b_buySix = GameInstance.instance.SixEngine;
	}

	void FixedUpdate()
	{
		if (b_buyEight)
		{
			EightEngine.SetActive(true);
			GameInstance.instance.EightEngine = true;
		}

		if (b_buySix)
		{
			SixEngine.SetActive(true);
			GameInstance.instance.SixEngine = true;
		}
		MoveInput();	
	}

	void MoveInput()
	{

		if (Input.GetKeyDown(KeyCode.K))
		{
			_carMoveSystem.rb.AddForce(transform.forward * 10000, ForceMode.Impulse);
		}
		float motorTorque = Input.GetAxis("Vertical");
		float steer = Input.GetAxis("Horizontal");
		bool isbreak = Input.GetKey(KeyCode.Space);

		_carMoveSystem.MoveWheel(motorTorque, steer, isbreak);
	}

	private void OnCollisionEnter(Collision collision)
	{
		GameManager.Instance.SoundManager.StopAllSFX();
		GameManager.Instance.SoundManager.PlaySFX("Crush");
		// 들어온 콜라이더의 방향을 가져옴
		UnityEngine.Vector3 direction = (collision.transform.position - transform.position).normalized;

		// 콜라이더의 방향을 기준으로 앞, 뒤, 오른쪽, 왼쪽을 판별함
		float dotForward = UnityEngine.Vector3.Dot(transform.forward, direction);
		float dotRight = UnityEngine.Vector3.Dot(transform.right, direction);

		// 방향을 기준으로 판별된 결과를 출력
		if (dotForward > 0.5f)
		{
			_carMoveSystem.Speed -= 0.1f;
			SpawnImpactEffect(collision.contacts[0].point);

		}
		else if (dotForward < -0.5f)
		{
			_carMoveSystem.rb.AddForce(transform.forward * 20000, ForceMode.Impulse);
			SpawnImpactEffect(collision.contacts[0].point);

		}
		else if (dotRight > 0.5f)
		{
			_carMoveSystem.Speed -= 0.05f;
			SpawnImpactEffect(collision.contacts[0].point);

		}

		else if (dotRight < -0.5f)
		{
			_carMoveSystem.Speed -= 0.05f;
			SpawnImpactEffect(collision.contacts[0].point);

		}
	}


	private void OnTriggerEnter(Collider other)
	{
		BaseItem item = other.GetComponent<BaseItem>();
		if (item != null)
		{
			item.OnGetItem(this);
			Destroy(other.gameObject);
			SoundManager.instance.PlaySFX("GetItem");
		}

		if (other.CompareTag("Home"))
		{
			Debug.Log("느려짐! 현재 속도: " + _carMoveSystem.Speed + ", 기본 속도: " + baseSpeed);
			_carMoveSystem.Speed = baseSpeed * 0.1f;
			Debug.Log("속도 변경 후: " + _carMoveSystem.Speed);

			// 여기에서 Coroutine을 호출합니다.
			StartCoroutine(ResetSpeedAfterDelay(2.0f));
		}
	}

	private void SpawnImpactEffect(UnityEngine.Vector3 position)
	{
		if (impactEffectPrefab != null)
		{
			GameObject impactEffect = Instantiate(impactEffectPrefab, position, Quaternion.identity);
			Destroy(impactEffect, 0.7f);
		}
	}


	IEnumerator ResetSpeedAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		_carMoveSystem.Speed = baseSpeed;
		Debug.Log("속도 복구됨: " + _carMoveSystem.Speed);
	}

}