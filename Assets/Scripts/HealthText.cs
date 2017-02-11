using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour {
	public GameObject playerObject;
	Player player;
	Text text;

	// on start, capture the needed component objects to avoid future calls
	void Start () {
		player = playerObject.GetComponent<Player> ();
		text = GetComponent<Text> ();
	}
	
	// continually update text based on player's health
	void Update () {
		text.text = "Health: " + player.health + " / " + player.maxHealth;
	}
}
