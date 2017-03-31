using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SliceNDice : Ability {
	public float bleedDuration;
	public float bleedDamage;
	List<GameObject> enemiesHit;

	// on start, initialize list of targeted enemies
	void Start() {
		enemiesHit = new List <GameObject> ();
	}

	// attack the enemy over which the mouse is positioned, dealing damage and applying bleed damage over time
	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {
		// only activate when cooldown is completed
		if (cooldownTimer != 0f) {
			return;
		}

		// use a raycast to determine what object the mouse is over
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		GameObject target = hit.transform.gameObject;
		// if said object is an enemy target, do damage and apply bleeding
		if (targets.Contains (target)) {
			// if enemy dies from the damage, remove it from the target list
			if (target.GetComponent<Enemy> ().TakeDamage (damage)) {
				targets.Remove (target);
			}
			// otherwise, make it bleed
			else {
				target.GetComponent<Enemy> ().takeDamageOverTime (bleedDuration, bleedDamage);
				enemiesHit.Add (target);
			}
		}

		// begin cooldown timer
		cooldownTimer = cooldown;
	}

	// passively track each enemy to which a bleed effect has been applied; if/when one dies, remove it from the targets list
	override public void Passive (Player player, List<GameObject> targets){
		// count down cooldown timer
		cooldownTimer = Mathf.Max (0f, cooldownTimer - Time.deltaTime);

		// remove enemies which have died but are still in the target list
		if (enemiesHit.Count > 0) {
			foreach (GameObject enemy in enemiesHit) {
				// check if enemy has been destroyed (will be null) but is still in target list
				if (enemy == null && targets.Contains (enemy)) {
					targets.Remove (enemy);
				}
			}
			// finally, remove all null enttries from list of hit enemies
			enemiesHit.RemoveAll (isNull);
		}
	}
}
