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



}
