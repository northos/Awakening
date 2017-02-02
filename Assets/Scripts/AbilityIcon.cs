using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityIcon : MonoBehaviour {
	public GameObject player;
	public int slot;

	// set this icon based on the player's skills and what slot this icon is for
	void Start () {
		switch (slot) {
		case 1:
			GetComponent<Image> ().sprite = player.GetComponent<Player> ().ability1.GetComponent<Ability>().icon;
			break;
		case 2:
			GetComponent<Image> ().sprite = player.GetComponent<Player> ().ability2.GetComponent<Ability>().icon;
			break;
		case 3:
			GetComponent<Image> ().sprite = player.GetComponent<Player> ().ability3.GetComponent<Ability>().icon;
			break;
		default:
			GetComponent<Image> ().sprite = player.GetComponent<Player> ().ability4.GetComponent<Ability>().icon;
			break;
		}
	}
}
