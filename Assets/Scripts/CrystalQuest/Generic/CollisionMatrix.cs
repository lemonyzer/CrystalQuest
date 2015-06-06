using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CollisionMatrix : ScriptableObject {

	[SerializeField]
	private bool[] matrix;



	void OnEnable()
	{
		if (matrix == null)
		{
			FirstInit();
		}
	}

	void FirstInit() {

	}

	void UpdateMatrix ()
	{
		// http://www.plyoung.com/blog/define-unity-layers-in-script.html
		// http://stackoverflow.com/questions/13363062/unity-custom-editor-like-layer-collision-matrix
		// http://answers.unity3d.com/questions/558158/how-to-get-a-list-from-user-layers.html
	}

}
