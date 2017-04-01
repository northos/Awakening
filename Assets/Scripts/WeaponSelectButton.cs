using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
[RequireComponent (typeof (Image))]

public class WeaponSelectButton : MonoBehaviour {
	public Text descriptionText;
	public string description;
	public string sceneTarget;
	public Button abilitySelectButton;
	List <Image> buttons;

	// on start, find all other tagged buttons in the scene
	void Start () {
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Button");
		buttons = new List<Image> ();
		foreach (GameObject t in temp) {
			if (t != gameObject) {
				buttons.Add (t.GetComponent <Image> ());
			}
		}
	}

	// select this weapon for the player to use
	// highlights this button and de-highlights the others
	// also makes sure the "ability select" button is active, and will transition to the appropriate scene
	public void selectWeapon () {
		// grey out all buttons
		foreach (Image button in buttons) {
			button.color = Color.gray;
		}
		// set this button to be highlighted (white)
		GetComponent <Image> ().color = Color.white;

		// update the start button to be interactable and transition to the proper scene
		abilitySelectButton.interactable = true;
		abilitySelectButton.GetComponent <SceneTransition> ().sceneName = sceneTarget;
	}


	// display text with the ability's description while the button is moused over
	// methods are public to word with Event Triggers
	public void showDescription () {
		descriptionText.text = description;
		descriptionText.enabled = true;
	}

	// hide said description text when not moused over
	public void hideDescription () {
		descriptionText.enabled = false;
	}
}
