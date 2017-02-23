using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectRow : MonoBehaviour {
	List<GameObject> buttons;
	public int row;
	public SkillSelectRow otherRow1;
	public SkillSelectRow otherRow2;
	public Button startButton;
	public bool active = false;

	// on start, get the child objects in order to quickly modify them as clicked
	void Start () {
		SkillSelectButton[] temp = GetComponentsInChildren<SkillSelectButton> ();
		buttons = new List<GameObject> ();
		foreach (SkillSelectButton b in temp) {
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
			SkillSelectButton skill = b.GetComponent<SkillSelectButton> ();
			// deactivate child buttons, if any
			if (skill.child1 != null) {
				skill.child1.setActive (false);
			}
			if (skill.child2 != null) {
				skill.child2.setActive (false);
			}
		}
		// lighten the button that was clicked and activate its children
		// obviously, don't do this if the arg button is null
		if (button != null) {
			button.GetComponent<Image> ().color = Color.white;
			SkillSelectButton activeSkill = button.GetComponent<SkillSelectButton> ();
			// deactivate child buttons, if any
			if (activeSkill.child1 != null) {
				activeSkill.child1.setActive (true);
			}
			if (activeSkill.child2 != null) {
				activeSkill.child2.setActive (true);
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
