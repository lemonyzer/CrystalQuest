using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Nasty {
	public string name;
	public Sprite icon;
	public bool isOn;
}

public class CreateNastiesScrollList : MonoBehaviour {

	public GameObject scrollViewContent;
	public GameObject scrollContentPrefab;
	public List<Nasty> nastiesList;

	// Use this for initialization
	void Start () {
		PopulateList ();
	}

	void PopulateList ()
	{
		foreach(Nasty nasty in nastiesList)
		{
			GameObject newScrollContent = Instantiate (scrollContentPrefab) as GameObject;
			NastyScrollItemScript itemScript = newScrollContent.GetComponent<NastyScrollItemScript>();
			itemScript.itemTextName.text = nasty.name;
			itemScript.itemImage.sprite = nasty.icon;
			itemScript.itemToggle.isOn = nasty.isOn;
			newScrollContent.transform.SetParent(scrollViewContent.transform,false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
