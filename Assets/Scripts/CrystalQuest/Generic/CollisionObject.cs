using UnityEngine;


[System.Serializable]
public class CollisionObject : ScriptableObject
{
	[SerializeField]
	private float damageValue = 0;
	//[SerializeField]
	//private float healthValue = 

	public virtual void Triggered (CollisionObject otherCollisionObject)
	{
	}
	
	public virtual void Collided ()
	{
	}
	
	public virtual void Collected ()
	{
	}
	
	public virtual void Collecting ()
	{
	}
}
