#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

public class WaveEditorScript {

	[MenuItem("Assets/CrystalQuest/Create Wave")]
	public static Wave CreateAsset ()
	{
		Wave asset = ScriptableObject.CreateInstance<Wave>();

		if (!AssetDatabase.IsValidFolder ("Assets"))
			AssetDatabase.CreateFolder ("", "Assets");

		if (!AssetDatabase.IsValidFolder ("Assets/ScriptableObjects"))
			AssetDatabase.CreateFolder ("Assets", "ScriptableObjects");

		AssetDatabase.CreateAsset (asset, "Assets/ScriptableObjects/Wave.asset");		//TODO
		AssetDatabase.SaveAssets ();

		return asset;
	}
	
	public static Wave CreateObject ()
	{
		Wave obj = ScriptableObject.CreateInstance<Wave>();
		return obj;
	}

}
#endif