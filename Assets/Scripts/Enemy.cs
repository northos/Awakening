using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
[RequireComponent (typeof (Animator))]

// this serves as a base class for all enemy behavior scripts in the game
// more specific enemies will need to have their own scripts which inherit from this and define specific behavior
public abstract class Enemy : MonoBehaviour {
	public float health;
	public float maxHealth;
	protected float damageReduction;
	public GameObject damageText;
	protected GameObject player;
	protected Animator animator;
	protected LayerMask enemyMask;

	// apply a given amount of damage to this enemy
	// returns whether or not this kills the enemy
	public bool TakeDamage (float damage) {
		health -= damage;
		damageText.GetComponent<TextMesh> ().text = ((int)damage).ToString();
		damageText.GetComponent<TextMesh> ().color = Color.white;
		Instantiate (damageText, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z - 1f), transform.rotation);
		// die if health reaches 0 and return true
		if (health <= 0.0f) {
			Destroy (gameObject);
			return true;
		}
		// if survived
		return false;
	}

	// creates a numerical modifier for the enemy's damage
	// the float given is the REDUCTION, not a percentage scale. so if a value of 10 is given, the enemy will do 90% damage
	// only the highest reduction can apply at once; if a lower reduction is applied, it does nothing
	public void reduceDamage (float reduction) {
		if (reduction > damageReduction) {
			damageReduction = reduction;
		}
	}

	// set enemy's starting stats
	// capture some objects in variables for future use
	// set up a layermask for pathfinding raycasts
	protected void Start () {
		health = maxHealth;
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = GetComponent<Animator> ();
		// create a layermask which checks only the "Default" layer (enemies should be on "Enemies" layer 8)
		int mask = LayerMask.GetMask (layerNames: "Default");
		enemyMask = mask;
	}
	
	// there is no basic update funcionality to be defined, so all of it will have to come from child classes
	abstract protected void Update ();

	// dim sprite slightly when the player mouses over
	public void OnMouseEnter () {
		GetComponent<SpriteRenderer> ().color = Color.gray;
	}

	// remove the dimming when the player mouses off
	public void OnMouseExit () {
		GetComponent<SpriteRenderer> ().color = Color.white;
	}
}
