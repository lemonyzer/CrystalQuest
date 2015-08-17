using UnityEngine;
using System.Collections;

public class AspectRatioScript : MonoBehaviour {
	
	public Vector2 targetAspect;

	[SerializeField]
	float windowAspect;

	[SerializeField]
	float scaleHeight;
	
	void Start () 
	{
		windowAspect = (float)Screen.width / (float)Screen.height;
		scaleHeight = windowAspect / (targetAspect.x/targetAspect.y);
		Camera camera = GetComponent<Camera>();
		
		if (scaleHeight < 1.0f)
		{  
			camera.orthographicSize = camera.orthographicSize / scaleHeight;
		}
	}
}
