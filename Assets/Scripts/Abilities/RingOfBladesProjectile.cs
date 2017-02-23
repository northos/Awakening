using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingOfBladesProjectile : MonoBehaviour {
	public float flySpeed;
	public float damage;
	public Vector3 direction;
	public Player player;
	public List<GameObject> targets;

	// on colliding with an object, damage it and destroy the projectile
	// enemies can be hit multiple times this way, but only if the projectile exits and re-enters
	void OnTriggerEnter2D (Collider2D c) {
		if (c.gameObject.tag == "Enemy") {
			if (c.gameObject.GetComponent<Enemy> ().TakeDamage (damage))
				// remove any targets which are killed from the list of targets
				targets.Remove (c.gameObject);
			// destroy this upon hitting an enemy
			Destroy (gameObject);
		}
	}

	// move projectile as follows:
	void Update () {
	}
}
