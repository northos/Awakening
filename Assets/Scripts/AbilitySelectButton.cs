using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]

public class AbilitySelectButton : MonoBehaviour {
	public AbilitySelectButton child1 = null;
	public AbilitySelectButton child2 = null;
	public Ability ability;
	public Text descriptionText;

	// on start, set the button's image to the attached ability's icon
	void Start () {
		GetComponent<Image> ().sprite = ability.icon;
	}

	// toggle this ability button's state to be active or inactive
	// this will affect the button's image color, and potentially activate or deactivate buttons below it on the tree
	public void setActive (bool active) {
		// set this buttons interactability based on the arg
		GetComponent<Button> ().interactable = active;
		// if this button has children (is not in the last row), set each of them to have the same interactability
		if (child1 != null) {
			child1.setActive (active);
		}
		if (child2 != null) {
			child2.setActive (active);
		}
	}

	// display text with the ability's description while the button is moused over
	// methods are public to word with Event Triggers
	public void showDescription () {
		descriptionText.text = ability.description;
		descriptionText.enabled = true;
	}

	// hide said description text when not moused over
	public void hideDescription () {
		descriptionText.enabled = false;
	}
}
