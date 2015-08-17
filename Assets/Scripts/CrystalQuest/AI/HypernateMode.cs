using UnityEngine;
using System.Collections;

public class HypernateMode : MonoBehaviour {


	[SerializeField]
	float hypernateIntervall = 3f;

	[SerializeField]
	float hypernateDuration = 3f;

	[SerializeField]
	float autoExitHypernateTimestamp = 0f;

	[SerializeField]
	bool hypernating = false;

	public bool Hypernating {
		get {return hypernating;}
		set {
			if (value != hypernating)
			{
				hypernating = value;

				if (hypernating)
				{
					NotifyEnterHypernating ();
				}
				else
					NotifyExitHypernating ();
			}
		}
	}

	[SerializeField]
	MyEvent enterHypernating;

	[SerializeField]
	MyEvent exitHypernating;

	[SerializeField]
	float nextPossibleHypernate = 0f; 

	public void TriggerHypernating ()
	{
		if (Time.time >= nextPossibleHypernate)
		{
			nextPossibleHypernate = Time.time + hypernateIntervall;
			autoExitHypernateTimestamp = Time.time + hypernateDuration;
			Hypernating = true;
		}

	}

	void Update ()
	{
		if (hypernating)
		{
			if (Time.time >= autoExitHypernateTimestamp)
				Hypernating = false;
		}
	}

	void NotifyEnterHypernating ()
	{
		enterHypernating.Invoke ();
	}

	void NotifyExitHypernating ()
	{
		exitHypernating.Invoke ();
	}
}
