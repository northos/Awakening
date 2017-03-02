using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathSpiralProjectile : MonoBehaviour {
	public float maxRadius;
	public float flySpeed;
	public float turnRate;
	public float turnDecay;
	public float damage;
	public Vector3 direction;
	public Player player;
	public List<GameObject> targets;
	public float duration;
	Vector3 origin;

	// on start, store the projectile's starting location in order to destroy it once it passes a certain distance from there
	void Start () {
		origin = transform.position;
	}

	// on colliding with an object, damage it
	// enemies can be hit multiple times this way, but only if the projectile exits and re-enters
	void OnTriggerEnter2D (Collider2D c) {
		if (c.gameObject.tag == "Enemy") {
			if (c.gameObject.GetComponent<Enemy> ().TakeDamage (damage))
				// remove any targets which are killed from the list of targets
				targets.Remove (c.gameObject);
		}
		// since this object will only have a Trigger collider, it will not interact with walls
		// this section destroys the projectile upon hitting a wall, rather than letting it fly through
		else if (c.gameObject.tag == "Wall") {
			//Destroy (gameObject);
		}
	}

	// move projectile as follows:
	//  * if the projectile has passed the maximum radius, destroy it
	//  * rotate the projectile clockwise by its current rotation rate
	//  * decay the rotation rate?
	//  * finally, move in the appropriate direction
	void Update () {
		// destroy they projectile if it's passed the maximum radius
		if (Vector3.Distance (transform.position, origin) >= maxRadius) {
			Destroy (gameObject);
		}

		// rotate the projectile clockwise by its turn rate
		direction = Quaternion.AngleAxis (turnRate * Time.deltaTime, Vector3.forward) * direction;

		// decay the turn rate, allowing the projectile to more gradually spiral out
		turnRate *= turnDecay;

		// once other parameters are updated, move the projectile forward at the proper speed
			transform.position += direction.normalized * flySpeed * Time.deltaTime;
	}
}
