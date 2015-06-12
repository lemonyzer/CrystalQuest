using UnityEngine;
using System.Collections;

public class CrystalObject : CollectableObject {

	// override default values in vererbung wird von unity serialisation nicht unterstützt
	// Error: The same field name is serialized multiple times in the class or its parent class. This is not supported: Base(CrystalObject) collisionSendDamageValue

	// http://forum.unity3d.com/threads/what-does-this-error-mean-exactly.122136/#post-1715684

//	[SerializeField]
//	protected new float currentHealth = 0f;
//	[SerializeField]
//	protected new float collisionSendDamageValue = 0f;

	protected override void Start ()
	{
		base.Start ();
		NotifyCrystalCreatedListener ();
	}

	protected override void OnDestroy ()
	{
		base.OnDestroy ();
		NotifyCrystalCollectedListener ();
	}

	public override void Die ()
	{
		base.Die ();
		NotifyCrystalCollectedListener ();
	}

	#region delegates
	public delegate void CrystalCreated (CrystalObject myObjectScript);
	public static event CrystalCreated onCrystalCreated;
	
	void NotifyCrystalCreatedListener ()
	{
		if (onCrystalCreated != null)
		{
			onCrystalCreated (this);
		}
		else
		{
			Debug.LogError ("no onCrystalCreated listener");
		}
	}

	public delegate void CrystalCollected (CrystalObject myObjectScript);
	public static event CrystalCollected onCrystalCollected;
	
	void NotifyCrystalCollectedListener ()
	{
		if (onCrystalCollected != null)
		{
			onCrystalCollected (this);
		}
		else
		{
			Debug.LogError ("no onCrystalCollected listener");
		}
	}
	#endregion
}
