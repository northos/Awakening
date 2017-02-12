using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamehead : Enemy {
	public float detectRange;
	public float attackRange;
	public float moveSpeed;

	// behavior for Flamehead enemies:
	//  * if player is within <detectRange>, move towards player
	//  * if player is within <attackRange>, charge attack (duration based on animation)
	//  * once attack is charged, attack in the direction of the player
	//    * if player is still in range, inflict damage
	//    * remain in attack stance until animation finishes
	//  * otherwise, wander slowly while player is too far away
	protected override void Update () {
		if (Vector3.Distance (transform.position, player.transform.position) <= attackRange) {
			animator.SetBool ("Walking", false);
			animator.SetBool ("Charging", true);
		}
	}

	// public function to perform attack on player
	// called as an event as the "prep" animation ends
	public void attack () {
		animator.SetBool ("Charging", false);
		animator.SetBool ("Attacking", true);
	}

	// public function to end attack on player
	// called as an event as the "attack" animation ends
	public void endAttack() {
		animator.SetBool ("Attacking", false);
	}
}
