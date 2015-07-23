#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

public class WaveEnemyEditorScript {

	[MenuItem("Assets/CrystalQuest/Create Wave Enemy")]
	public static Enemy CreateAsset ()
	{
		Enemy asset = ScriptableObject.CreateInstance<Enemy>();

		string absPath = System.IO.Path.GetFullPath (Application.dataPath);
		string relPath = "Assets";

//		if (!AssetDatabase.IsValidFolder ("Assets"))
//			AssetDatabase.CreateFolder ("", "Assets");

//		Debug.Log (AssetDatabase.IsValidFolder (absPath));
//		Debug.Log (AssetDatabase.IsValidFolder (relPath));

		if (!AssetDatabase.IsValidFolder (relPath))
		{
			Debug.Log ("creating " + relPath);
			AssetDatabase.CreateFolder ("", "Assets");
		}

		absPath += "/ScriptableObjects";
		relPath += "/ScriptableObjects";

		if (!AssetDatabase.IsValidFolder (relPath))
		{
			Debug.Log ("creating " + relPath);
			AssetDatabase.CreateFolder ("Assets", "ScriptableObjects");
		}

		AssetDatabase.CreateAsset (asset, relPath + "/Enemy.asset");		//TODO
		AssetDatabase.SaveAssets ();

		return asset;
	}
	
	public static Enemy CreateObject ()
	{
		Enemy obj = ScriptableObject.CreateInstance<Enemy>();
		return obj;
	}

}
#endif