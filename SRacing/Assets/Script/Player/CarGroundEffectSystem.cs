using UnityEngine;

public class CarGroundEffectSystem : MonoBehaviour
{
	private TerrainDetector terrainDetector;
	private CarMoveSystem moveSystem;
	public float baseSpeed = 10f;
	public GameObject SlowPage;
	
	void Start()
	{
		moveSystem = GetComponent<CarMoveSystem>();
		terrainDetector = new TerrainDetector();
	}

	void Update()
	{
		int activeTerrainTextureIdx = terrainDetector.GetActiveTerrainTextureIdx(transform.position);
		switch (activeTerrainTextureIdx)
		{
			case 0:
				moveSystem.Speed = baseSpeed;
				SlowPage.gameObject.SetActive(false);
				break;
			default:
				moveSystem.Speed = baseSpeed * 0.6f;
				SlowPage.gameObject.SetActive(true);
				break;
		}

	}

	

}
