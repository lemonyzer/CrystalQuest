using UnityEngine;
using System.Collections;

public class AreaMovement : MonoBehaviour {

	#region Area
	
	[SerializeField]
	protected Vector3 areaCenterPosition = Vector3.zero;		// 
	
	[SerializeField]
	protected bool interruptMovingToAreaCenter = true;		// 
	
	[SerializeField]
	protected float areaCenterMinDistanceToReachEnable = 0.25f;		// wie schnell soll der Ort gewechselt werden
	
	[SerializeField]
	protected float changeAreaInterval = 2f;		// wie schnell soll der Ort gewechselt werden
	
	[SerializeField]
	protected float nextChangeAreaTimestamp = 0f;		// wann wird nächste Area festgelegt
	
	[SerializeField]
	protected float nextAreaDistanceMin = 2f;		// wie weit ist die nächste Area mindestns entfernt?
	[SerializeField]
	protected float nextAreaDistanceMax = 5f;		// wie weit ist die nächste Area mindestns entfernt?
	
	[SerializeField]
	protected float nextAreaDistance = 0f;		// wie weit ist die nächste Area mindestns entfernt?
	
	[SerializeField]
	protected bool moveingToNextArea = false;	
	/*
	 * übergeordnet:
	 * 		- KI wechselt zwischen Bewegungsarten
	 */
	#endregion

}
