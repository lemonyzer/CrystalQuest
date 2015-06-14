using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class HealthManager : MonoBehaviour {

	[SerializeField]
	float health;

	[SerializeField]
	int lifes;

	[SerializeField]
	bool invincible;

	#region collision trigger AKA Health Modifiing Trigger 
//	[SerializeField]
//	List<MyFloatEvent> healthModifiyingTrigger;		<-- TODO #1 Problem: kann nicht im Inspector ausgefüllt werden 

//	[SerializeField]
//	List<ScriptWithFloatEvent> healthModifyingTrigger;		<-- TODO #1 Lösung

	[SerializeField]
	public List<CollisionTrigger> collisionTriggerScripts;

	[SerializeField]
	public UnityAction<float> myCollisionTriggerListener;
	#endregion 

	void Awake ()
	{
		myCollisionTriggerListener = new UnityAction<float> (Action);
	}

	void Action (float damageValue)
	{
//		health -= damageValue;
		Debug.Log ("BITCH please!");
		Debug.Log ("BITCH please!");
		Debug.Log ("BITCH please!");
		Debug.Log ("BITCH please!");
		Debug.Log ("BITCH please!");
		Debug.Log ("BITCH please!");
	}

	void StartTriggerListening ()
	{
		Debug.LogError (this.ToString () + " StartTriggerListening");
		if (collisionTriggerScripts != null)
		{
			for (int i = 0; i < collisionTriggerScripts.Count; i++)
			{
				collisionTriggerScripts[i].StartListening (myCollisionTriggerListener);
			}
		}
	}
	
	void StopTriggerListening ()
	{
		Debug.LogError (this.ToString () + " StopTriggerListening");
		if (collisionTriggerScripts != null)
		{
			for (int i = 0; i < collisionTriggerScripts.Count; i++)
			{
				collisionTriggerScripts[i].StopListening (myCollisionTriggerListener);
			}
		}
	}
	
	void OnEnable () {
		StartTriggerListening ();
	}
	
	void OnDisable ()
	{
		StopTriggerListening ();
	}
	
//	public void StartListening (UnityAction<float> floatListener)
//	{
//		myEvent.AddListener (listener);
//	}
//	
//	public void StopListening (UnityAction<float> floatListener)
//	{
//		myEvent.RemoveListener (listener);
//	}
}
