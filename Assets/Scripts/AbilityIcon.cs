using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Image))]

public class AbilityIcon : MonoBehaviour {
	public GameObject player;
	Ability ability;
	public GameObject cooldownDisplay;
	Text cooldownText;
	Image image;
	public int slot;

	// set this icon based on the player's skills and what slot (1-4) this icon occupies
	// capture text, image, and Ability script components to avoid later calls
	void Start () {
		cooldownText = cooldownDisplay.GetComponent<Text> ();
		image = GetComponent<Image> ();
		switch (slot) {
		case 1:
			ability = player.GetComponent<Player> ().ability1.GetComponent<Ability> ();
			image.sprite = ability.icon;
			break;
		case 2:
			ability = player.GetComponent<Player> ().ability2.GetComponent<Ability> ();
			image.sprite = ability.icon;
			break;
		case 3:
			ability = player.GetComponent<Player> ().ability3.GetComponent<Ability> ();
			image.sprite = ability.icon;
			break;
		default:
			ability = player.GetComponent<Player> ().ability4.GetComponent<Ability> ();
			image.sprite = ability.icon;
			break;
		}
	}

	// update the display based on ability cooldowns as they activate and expire
	void Update () {
		// while ability is on cooldown:
		//  * darken icon to gray
		//  * display cooldown timer text
		//  * update cooldown timer text with # of seconds left
		if (ability.cooldownTimer > 0f) {
			image.color = Color.gray;
			cooldownText.enabled = true;
			cooldownText.text = "" + Mathf.CeilToInt (ability.cooldownTimer);
		}
		// when ability is not on cooldown, reset color to white and disable text
		else {
			image.color = Color.white;
			cooldownText.enabled = false;
		}
	}
}
