using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Collisions : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider; 
	int points = 0;

	public Text pointsText;

	void Awake()
	{
		currentHealth = startingHealth;
		healthSlider.value = currentHealth;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Crystal")
		{
			points++;
			pointsText.text = "Points " + points;
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "Enemy")
		{
			points = 0;
			currentHealth -= 10;
			if(currentHealth >=0)
			{
				healthSlider.value = currentHealth;
			}
			if(currentHealth <= 0)
			{
				Application.LoadLevel(Application.loadedLevel);
			}

		}
	}

	void OnGUI()
	{
		GUILayout.Box("Points: " + points.ToString());
	}
}
