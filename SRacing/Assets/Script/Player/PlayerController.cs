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
		// ���� �ݶ��̴��� ������ ������
		UnityEngine.Vector3 direction = (collision.transform.position - transform.position).normalized;

		// �ݶ��̴��� ������ �������� ��, ��, ������, ������ �Ǻ���
		float dotForward = UnityEngine.Vector3.Dot(transform.forward, direction);
		float dotRight = UnityEngine.Vector3.Dot(transform.right, direction);

		// ������ �������� �Ǻ��� ����� ���
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
			Debug.Log("������! ���� �ӵ�: " + _carMoveSystem.Speed + ", �⺻ �ӵ�: " + baseSpeed);
			_carMoveSystem.Speed = baseSpeed * 0.1f;
			Debug.Log("�ӵ� ���� ��: " + _carMoveSystem.Speed);

			// ���⿡�� Coroutine�� ȣ���մϴ�.
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
		Debug.Log("�ӵ� ������: " + _carMoveSystem.Speed);
	}

}