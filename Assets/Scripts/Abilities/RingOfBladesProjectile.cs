using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody2D))]

public class RingOfBladesProjectile : MonoBehaviour {
	public float flySpeed;
	public float damage;
	public float orbitDistance;
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

	// each frame, move projectile as follows:
	//  * get a vector towards the player
	//  * find the vector 90 degrees clockwise from that
	//  * move the projectile along the second vector according to its speed; this will make it move in a circle around the player
	//  * move the projectile back to the proper distance from the player, ensuring it stays in a consistent circular path
	//  * no need to rotate the projectile; it has an animation to handle that
	void Update () {
		// get a vector towards the player
		Vector3 playerVector = player.transform.position - transform.position;
		// find the vector 90 degrees clockwise from the player vector
		Vector3 moveVector = Quaternion.AngleAxis (90, Vector3.forward) * playerVector;
		// move the projectile along this vector according to its speed
		transform.position += moveVector.normalized * flySpeed  * Time.deltaTime;
		// move the projectile back to the porper distance from the player
		// (first update the vector to the player for the projectile's new position...
		// (then scale it to the proper distance and set the position to be the player's position plus that vector)
		playerVector = (transform.position - player.transform.position).normalized * orbitDistance;
		transform.position = player.transform.position + playerVector;
	}
}
