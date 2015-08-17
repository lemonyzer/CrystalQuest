#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

public class WaveSystemEditorScript : EditorWindow {

	[MenuItem("Assets/Create/Wave List")]
	public static WaveSystem Create ()
	{
		WaveSystem asset = ScriptableObject.CreateInstance<WaveSystem> ();

		AssetDatabase.CreateAsset (asset, "Assets/WaveSystem.asset");
		AssetDatabase.SaveAssets ();
		return asset;
	}

	public WaveSystem waveList;
	private int viewIndex = 1;

	[MenuItem ("Window/Wave System Editor %#e")]
	static void Init ()
	{
		EditorWindow.GetWindow (typeof (WaveSystemEditorScript));
	}

	string EP_LastWaveList = "LastWaveList";

	void OnEnable () {
		if (EditorPrefs.HasKey (EP_LastWaveList)) {
			string objectPath = EditorPrefs.GetString (EP_LastWaveList);
			waveList = AssetDatabase.LoadAssetAtPath (objectPath, typeof(WaveSystem)) as WaveSystem;
		}
	}

	void OnGUI ()
	{
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Wave System Editor", EditorStyles.boldLabel);
		if (waveList != null) {
			if (GUILayout.Button ("Show Wave List")) {
				EditorUtility.FocusProjectWindow ();
				Selection.activeObject = waveList;
			}
		}
		if (GUILayout.Button ("Open Wave List")) {
			OpenWaveList();
		}
		if (GUILayout.Button ("New Wave List")) {
			CreateNewWaveList ();		// TODO rename if exists
		}

		GUILayout.EndHorizontal ();

		if (waveList == null) {
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			if (GUILayout.Button ("Create New Wave List", GUILayout.ExpandWidth (false))) {
				CreateNewWaveList ();		// TODO rename if exists
			}
			if (GUILayout.Button ("Open Existing Wave List", GUILayout.ExpandWidth (false))) {
				OpenWaveList ();
			}
			GUILayout.EndHorizontal ();
		}

		GUILayout.Space (20);

		if (waveList != null) {
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			
			if (GUILayout.Button("Prev", GUILayout.ExpandWidth (false))) {
				if (viewIndex >1)
					viewIndex --;
			}
			GUILayout.Space (5);
			
			if (GUILayout.Button("Next", GUILayout.ExpandWidth (false))) {
				if (viewIndex < waveList.waveList.Count)
					viewIndex ++;
			}

			GUILayout.Space (60);
			
			if (GUILayout.Button("Add Wave", GUILayout.ExpandWidth (false))) {
				AddWave ();
			}
			if (GUILayout.Button("Delete Wave", GUILayout.ExpandWidth (false))) {
				DeleteWave (viewIndex-1);
			}

			GUILayout.EndHorizontal ();

			if (waveList.waveList.Count > 0) {
				GUILayout.BeginHorizontal ();
				viewIndex = Mathf.Clamp (EditorGUILayout.IntField ("Current Wave", viewIndex, GUILayout.ExpandWidth (false)), 1, waveList.waveList.Count);
				EditorGUILayout.LabelField ("of\t" + waveList.waveList.Count.ToString () + "\tWaves", "", GUILayout.ExpandWidth (false));
				GUILayout.EndHorizontal ();
			}

			crystalStartValue = EditorGUILayout.IntField ("Crystal start Amount", crystalStartValue, GUILayout.ExpandWidth (false));
			crystalGrowValue = EditorGUILayout.IntField ("Crystal grow Amount", crystalGrowValue, GUILayout.ExpandWidth (false));
			mineMinWaveNr = EditorGUILayout.IntField ("Mine start Wave ", mineMinWaveNr, GUILayout.ExpandWidth (false));
			mineStartValue = EditorGUILayout.IntField ("Mine start Amount ", mineStartValue, GUILayout.ExpandWidth (false));
			mineGrowValue = EditorGUILayout.IntField ("Mine grow Amount", mineGrowValue, GUILayout.ExpandWidth (false));
			if (GUILayout.Button("Init Waves", GUILayout.ExpandWidth (false))) {
				for (int i=0; i< waveList.waveList.Count; i++)
				{
					waveList.waveList[i].crystalAmount = crystalStartValue + i*crystalGrowValue;

					if (i > mineMinWaveNr )
						waveList.waveList[i].mineAmount = mineStartValue + (i-mineMinWaveNr)*mineGrowValue;
				
					EditorUtility.SetDirty (waveList.waveList[i]);
					
				}
				EditorUtility.SetDirty (waveList);
			}

			if (GUILayout.Button ("save Waves and List", GUILayout.ExpandWidth (false))) {
				AssetDatabase.SaveAssets ();
			}
		}


		
	}

	public int crystalStartValue = 4;
	public int crystalGrowValue = 2;

	public int mineStartValue = 2;
	public int mineMinWaveNr = 4;
	public int mineGrowValue = 2;

	// TODO rename if exists
	void CreateNewWaveList ()
	{
		// No overwrite protection !!!
		viewIndex = 1;
		waveList = Create();
		if (waveList) {
			string relPath = AssetDatabase.GetAssetPath (waveList);
			EditorPrefs.SetString (EP_LastWaveList, relPath);
		}
	}

	void OpenWaveList ()
	{
		string absPath = EditorUtility.OpenFilePanel ("Select Wave List", "", "");
		if (absPath.StartsWith (Application.dataPath)) {
			string relPath = absPath.Substring (Application.dataPath.Length - "Assets".Length);
			waveList = AssetDatabase.LoadAssetAtPath (relPath, typeof(WaveSystem)) as WaveSystem;
			if (waveList) {
				// wenn waveList asset erfolgreich geladen, speichere Einstellung in EditorPrefs
				EditorPrefs.SetString (EP_LastWaveList, relPath);
			}
		}
	}

	void AddWave () {
		Wave newWave = WaveEditorScript.CreateAsset ();
		string waveName = waveList.waveList.Count + ". Wave";
		AssetDatabase.RenameAsset (AssetDatabase.GetAssetPath(newWave), waveName);
		newWave.waveName = waveName;
		waveList.waveList.Add (newWave);
		viewIndex = waveList.waveList.Count;
	}

	void DeleteWave (int index) {
		waveList.waveList.RemoveAt (index);
	}

}
#endif