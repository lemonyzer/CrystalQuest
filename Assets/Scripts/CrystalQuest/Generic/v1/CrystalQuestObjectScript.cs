using UnityEngine;
using System.Collections;

public class CrystalQuestObjectScript : MonoBehaviour {

	#region Override
	new public Transform transform;
	#endregion

	#region Initialisation
	protected virtual void Awake ()
	{
//		Debug.Log(this.name + " Awake() " + this.ToString());
		transform = this.GetComponent<Transform>();
//		InitializeRigidbody2D();
//		NotifyCreatedListener ();	// in Start, da CrystalQuestGameManager in Awake Liste initialisiert und in OnEnable erst "zuhört" und wir keine ScriptOrder setzen möchten
	}

	protected virtual void Start ()
	{
		NotifyCreatedListener ();	// in Start, da CrystalQuestGameManager in Awake Liste initialisiert und in OnEnable erst "zuhört" und wir keine ScriptOrder setzen möchten
	}
	#endregion

	#region Delegate
	public delegate void Created (CrystalQuestObjectScript me);
	public static event Created onCreated;

	public void NotifyCreatedListener ()
	{
		if (onCreated != null)
			onCreated (this);
		else
			Debug.LogError (this.ToString() + " no onCreated Listener");
	}

	public delegate void Destroyed (CrystalQuestObjectScript me);
	public static event Destroyed onDestroyed;

	public void NotifyDestroyedListener ()
	{
		if (onDestroyed != null)
			onDestroyed (this);
		else
			Debug.LogError (this.ToString() + " no onDestroyed Listener");
	}
	#endregion



	public virtual void RestartLevel ()
	{
		
	}

	public virtual void NextLevel (int level)
	{
		
	}

	protected virtual void OnDestroy ()
	{

	}

}
