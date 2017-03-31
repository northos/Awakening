using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallOfBlades : Ability {
	public float duration;
	float durationTimer;
	bool active = false;

	// when executed, activate shield which immobilizes the player but reflects all incoming damage
	// lasts for the assigned duration
	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {
		// only activate when cooldown is completed
		if (cooldownTimer != 0f || active) {
			return;
		}

		active = true;
		player.GetComponent <Animator> ().SetBool ("Immobilized", true);

		// begin cooldown timer
		cooldownTimer = cooldown;
	}

	// while shield is active, player takes no damage and all damage is reflected to attacker
	override public float OnHit(Player player, GameObject attacker, float hitDamage) {
		if (active) {
			attacker.GetComponent <Enemy> ().TakeDamage (hitDamage);
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
	}
}
