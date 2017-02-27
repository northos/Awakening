using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelectRow : MonoBehaviour {
	List<GameObject> buttons;
	public int row;
	public AbilitySelectRow otherRow1;
	public AbilitySelectRow otherRow2;
	public Button startButton;
	public bool active = false;
	// field for the Player prefab, allowing the row of buttons to modify which abilitys the player will use
	public Player player;

	// on start, get the child objects in order to quickly modify them as clicked
	void Start () {
		AbilitySelectButton[] temp = GetComponentsInChildren<AbilitySelectButton> ();
		buttons = new List<GameObject> ();
		foreach (AbilitySelectButton b in temp) {
			buttons.Add (b.gameObject);
		}
	}

	// when a button in this row is clicked, select that one and deselect any others in the row
	// can be passed a null arg to deactivate the whole row
	public void Clicked (GameObject button) {
		// mark this row of buttons as active (unless arg button is null - then set false)
		active = button != null;
		// grey out all the buttons in the row
		// also deactivate the buttons below each of them
		foreach (GameObject b in buttons) {
			b.GetComponent<Image> ().color = Color.grey;
			AbilitySelectButton ability = b.GetComponent<AbilitySelectButton> ();
			// deactivate child buttons, if any
			if (ability.child1 != null) {
				ability.child1.setActive (false);
			}
			if (ability.child2 != null) {
				ability.child2.setActive (false);
			}
		}
		// lighten the button that was clicked and activate its children
		// obviously, don't do this if the arg button is null
		if (button != null) {
			button.GetComponent<Image> ().color = Color.white;
			AbilitySelectButton activeAbility = button.GetComponent<AbilitySelectButton> ();
			// assign the appropriate ability slot of the player to the chosen ability
			switch (row) {
			case 2:
				player.ability2 = button.GetComponent<AbilitySelectButton> ().ability.GetComponent<Ability> ();
				break;
			case 3:
				player.ability3 = button.GetComponent<AbilitySelectButton> ().ability.GetComponent<Ability> ();
				break;
			case 4:
				player.ability4 = button.GetComponent<AbilitySelectButton> ().ability.GetComponent<Ability> ();
				break;
			}
			// deactivate child buttons, if any
			if (activeAbility.child1 != null) {
				activeAbility.child1.setActive (true);
			}
			if (activeAbility.child2 != null) {
				activeAbility.child2.setActive (true);
			}

			// alert lower rows that this one has changed by "clicking" on a null button in that row
			// need to do this because changing the selection in this row invalidates any selections in the lower rows
			if (otherRow1.row > row) {
				otherRow1.Clicked (null);
			}
			if (otherRow2.row > row) {
				otherRow2.Clicked (null);
			}
		}

		// activate or deactivate the start button based on whether a selection has been made in all rows
		startButton.interactable = otherRow1.active && otherRow2.active;
	}
}
