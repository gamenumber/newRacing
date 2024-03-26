using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapManager : BaseManager
{
	public Camera mainCamera;
	public RawImage rawImage; 

	void Start()
	{
		RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
		mainCamera.targetTexture = renderTexture;
		rawImage.texture = renderTexture;
	}
}
