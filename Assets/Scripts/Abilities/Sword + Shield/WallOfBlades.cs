using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallOfBlades : Ability {
	public float duration;
	float durationTimer;
	bool active = false;
	List <GameObject> killed;

	// on start, initialize list of killed enemies
	void Start() {
		killed = new List <GameObject> ();
	}

	// when executed, activate shield which immobilizes the player but reflects all incoming damage
	// lasts for the assigned duration
	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {
		// only activate when cooldown is completed
		if (cooldownTimer != 0f || active) {
			return;
		}

		// activate shield, immobilize player, and start the duration timer
		active = true;
		player.GetComponent <Animator> ().SetBool ("Immobilized", true);
		durationTimer = duration;

		// begin cooldown timer
		cooldownTimer = cooldown;
	}

	// while shield is active, player takes no damage and all damage is reflected to attacker
	override public float OnHit(Player player, GameObject attacker, float hitDamage) {
		if (active) {
			// reflect damage back at attacker; if it dies, store it to be removed from targets
			if (attacker.GetComponent <Enemy> (). TakeDamage (hitDamage)) {
				killed.Add (attacker);
			}
			return 0f;
		}
		// if inactive, simply take normal damage
		return hitDamage;
	}

	// passively track duration and cooldown
	override public void Passive(Player player, List<GameObject> targets) {
		// count down cooldown timer
		cooldownTimer = Mathf.Max (0f, cooldownTimer - Time.deltaTime);
		// count down duration timer
		durationTimer = Mathf.Max (0f, durationTimer - Time.deltaTime);
		// when duration runs out, deactivate
		if (durationTimer == 0f) {
			active = false;
			player.GetComponent <Animator> ().SetBool ("Immobilized", false);
		}

		// if any enemies were killed since the last update, remove them from the targets list
		if (killed.Count > 0) {
			foreach (GameObject enemy in killed) {
				targets.Remove (enemy);
				killed.Remove (enemy);
			}
			// then clear the list
			killed.Clear ();
		}
	}
}
