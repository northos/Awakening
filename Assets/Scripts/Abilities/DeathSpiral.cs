using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathSpiral : Ability {
	public GameObject projectilePrefab;

	// spawns a projectile which will spiral out around the player, damaging all enemies it touches
	override public void Execute(Player player, Vector3 direction, List<GameObject> targets) {
		if (cooldownTimer != 0f)
			return;
		// spawn a shield projectile on the player
		GameObject newProjectile = (GameObject)Instantiate (projectilePrefab, player.transform.position, player.transform.rotation);
		newProjectile.GetComponent<DeathSpiralProjectile>().direction = direction;
		newProjectile.GetComponent<DeathSpiralProjectile>().player = player;
		newProjectile.GetComponent<DeathSpiralProjectile>().targets = targets;
		cooldownTimer = cooldown;
	}
}
