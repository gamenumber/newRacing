using UnityEngine;

public class AddMeshColliderToChildren : MonoBehaviour
{
	void Start()
	{
		AddMeshColliderRecursively(transform);
	}

	void AddMeshColliderRecursively(Transform parent)
	{
		foreach (Transform child in parent)
		{
			// 자식 오브젝트가 MeshFilter를 가지고 있는지 확인
			MeshFilter meshFilter = child.GetComponent<MeshFilter>();
			if (meshFilter != null)
			{
				// MeshCollider가 이미 추가되어 있는지 확인
				if (child.GetComponent<MeshCollider>() == null)
				{
					// MeshCollider 추가
					MeshCollider meshCollider = child.gameObject.AddComponent<MeshCollider>();
					// MeshCollider의 Mesh를 MeshFilter의 Mesh로 설정
					meshCollider.sharedMesh = meshFilter.sharedMesh;
				}
			}

			// 자식 오브젝트의 스케일 값을 반영
			Vector3 originalScale = child.localScale;
			child.localScale = Vector3.one; // 일단 스케일을 원점으로 되돌린 후 MeshCollider를 추가
											// 재귀적으로 자식 오브젝트의 자식들에게도 적용
			AddMeshColliderRecursively(child);
			// 스케일을 다시 원래대로 되돌림
			child.localScale = originalScale;
		}
	}
}
