using UnityEngine;
using System.Collections;

public class CrystalQuestObjectScript : MonoBehaviour {

	#region Override
	new public Transform transform;
	#endregion

	#region Initialisation
	protected virtual void Awake()
	{
		Debug.Log(this.name + " Awake() " + this.ToString());
		transform = this.GetComponent<Transform>();
//		InitializeRigidbody2D();
	}
	#endregion
	

	[SerializeField]
	protected float points;

	public float Points {
		get { return points; }
		set { points = value; }
	}
	

}
