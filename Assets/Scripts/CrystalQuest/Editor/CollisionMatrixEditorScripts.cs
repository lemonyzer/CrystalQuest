using UnityEngine;
using UnityEditor;
using System.Collections;

public class CollisionMatrixEditorScripts {

	[MenuItem("Assets/CrystalQuest/Create CollisionMatrix")]
	public static CollisionMatrix CreateCollisionMatrixAsset ()
	{
		CollisionMatrix asset = ScriptableObject.CreateInstance<CollisionMatrix>();

		AssetDatabase.CreateFolder ("", "Assets");
		AssetDatabase.CreateFolder ("Assets", "ScriptableObjects");
		AssetDatabase.CreateAsset (asset, "Assets/ScriptableObjects/CollisionMatrix.asset");		//TODO
		AssetDatabase.SaveAssets ();

		return asset;
	}
	
	public static CollisionMatrix CreateCollisionMatrix ()
	{
		CollisionMatrix obj = ScriptableObject.CreateInstance<CollisionMatrix>();
		return obj;
	}

}
