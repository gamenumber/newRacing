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
			// �ڽ� ������Ʈ�� MeshFilter�� ������ �ִ��� Ȯ��
			MeshFilter meshFilter = child.GetComponent<MeshFilter>();
			if (meshFilter != null)
			{
				// MeshCollider�� �̹� �߰��Ǿ� �ִ��� Ȯ��
				if (child.GetComponent<MeshCollider>() == null)
				{
					// MeshCollider �߰�
					MeshCollider meshCollider = child.gameObject.AddComponent<MeshCollider>();
					// MeshCollider�� Mesh�� MeshFilter�� Mesh�� ����
					meshCollider.sharedMesh = meshFilter.sharedMesh;
				}
			}

			// �ڽ� ������Ʈ�� ������ ���� �ݿ�
			Vector3 originalScale = child.localScale;
			child.localScale = Vector3.one; // �ϴ� �������� �������� �ǵ��� �� MeshCollider�� �߰�
											// ��������� �ڽ� ������Ʈ�� �ڽĵ鿡�Ե� ����
			AddMeshColliderRecursively(child);
			// �������� �ٽ� ������� �ǵ���
			child.localScale = originalScale;
		}
	}
}
