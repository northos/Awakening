using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamehead : Enemy {
	public float detectRange;
	public float attackRange;
	public float hitRange;
	public float moveSpeed;
	public float hitDamage; 
	Vector3 targetLocation = Vector3.zero;

	// behavior for Flamehead enemies:
	//  * if player is within <detectRange>, move towards player
	//  * if player is within <attackRange>, charge attack (duration based on animation)
	//  * once attack is charged, attack in the direction of the player
	//    * if player is still in range, inflict damage
	//    * remain in attack stance until animation finishes
	//  * otherwise, wander slowly while player is too far away
	protected override void Update () {
		// when player is within detection range, begin walking towards the player
		if (Vector3.Distance (transform.position, player.transform.position) <= detectRange && !animator.GetBool ("Charging") && !animator.GetBool ("Attacking")) {
			animator.SetBool ("Walking", true);
			// move at set walking speed towards player
			Vector3 moveVector = (player.transform.position - transform.position).normalized * moveSpeed;
			transform.Translate (moveVector * Time.deltaTime);
		}
		// when player is within attack range, begin chargingi an attack
		if (Vector3.Distance (transform.position, player.transform.position) <= attackRange && !animator.GetBool ("Charging") && !animator.GetBool ("Attacking")) {
			animator.SetBool ("Walking", false);
			animator.SetBool ("Charging", true);
		}
	}

	// public function to perform attack on player
	// called as an event as the "prep" animation ends
	// damage the player if they're within hit range (so hit area is a circle around the Flamehead)
	public void attack () {
		animator.SetBool ("Charging", false);
		animator.SetBool ("Attacking", true);
		if (Vector3.Distance (transform.position, player.transform.position) <= hitRange) {
			player.GetComponent<Player> ().TakeDamage (hitDamage, gameObject);
		}
	}

	// public function to end attack on player
	// called as an event as the "attack" animation ends
	public void endAttack() {
		animator.SetBool ("Attacking", false);
	}
}
