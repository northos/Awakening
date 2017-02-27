using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]

public class AbilitySelectButton : MonoBehaviour {
	public AbilitySelectButton child1 = null;
	public AbilitySelectButton child2 = null;
	public GameObject ability;

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
}
