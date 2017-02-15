using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public float health;
	public float maxHealth;
	public float walkSpeed;
	public float horizZone;
	public float vertZone;
	public GameObject mainCamera;
	public Vector3 target = Vector3.zero;
	public Ability ability1;
	public Ability ability2;
	public Ability ability3;
	public Ability ability4;
	List<GameObject> enemies;
	public Animator animator;
	public GameObject damageText;

	// Apply a given amount of damage to the player
	public void TakeDamage (float damage, GameObject attacker){
		// Modify incoming damage based on all abilities
		damage = ability4.OnHit (this, attacker, ability3.OnHit (this, attacker, ability2.OnHit (this, attacker, ability1.OnHit (this, attacker, damage))));
		health -= damage;
		damageText.GetComponent<TextMesh> ().text = ((int)damage).ToString();
		damageText.GetComponent<TextMesh> ().color = Color.red;
		Instantiate (damageText, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z - 1f), transform.rotation);
		// Die if health reaches 0
		if (health <= 0.0f) {
			
		}
	}

	// Set player's starting stats
	void Start () {
		health = maxHealth;
		enemies = new List<GameObject> ();
		enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
		animator = GetComponent<Animator> ();
	}

	// Move the player based on keyboard input
	void Update () {
		Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

		// check that the player is in movement mode (not immobilized or using a movement skill)
		if (!(animator.GetBool ("Immobilized"))) {
			// Check for mouse click, and give the player a target to move to at the mouse's position
			if (Input.GetMouseButton (0) || Input.GetMouseButton (1)) {
				target = (Vector3)mousePosition;
				animator.SetBool ("Walking", true);
			}
			// If there is a target, move towards it
			if (target != Vector3.zero) {
				Vector2 moveVector = (Vector2)(target - transform.position);
				transform.position += (Vector3)(moveVector.normalized * walkSpeed * Time.deltaTime);
				if (Vector2.Distance (target, transform.position) < walkSpeed * Time.deltaTime) {
					target = Vector3.zero;
					animator.SetBool ("Walking", false);
				}
			}

			// Move camera if the player has strayed too far from center
			// Vertically (up):
			if (transform.position.y - mainCamera.transform.position.y > vertZone) {
				mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, transform.position.y - vertZone, mainCamera.transform.position.z);
			}
			// Vertically (down):
			else if (mainCamera.transform.position.y - transform.position.y > vertZone) {
				mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, transform.position.y + vertZone, mainCamera.transform.position.z);
			}
			// Horizontally (right):
			if (transform.position.x - mainCamera.transform.position.x > horizZone) {
				mainCamera.transform.position = new Vector3 (transform.position.x - horizZone, mainCamera.transform.position.y, mainCamera.transform.position.z);
			}
			// Horizontally (left):
			else if (mainCamera.transform.position.x - transform.position.x > horizZone) {
				mainCamera.transform.position = new Vector3 (transform.position.x + horizZone, mainCamera.transform.position.y, mainCamera.transform.position.z);
			}
		}

		// Check for ability input
		Vector3 targetVector = ((Vector3)mousePosition - transform.position).normalized;
		// Ability 1
		if (Input.GetKeyDown ("1")) {
			ability1.Execute(this, targetVector, enemies);
		} else if (Input.GetKeyDown ("2")) {	// Ability 2
			ability2.Execute(this, targetVector, enemies);
		} else if (Input.GetKeyDown ("3")) {	// Ability 3
			ability3.Execute(this, targetVector, enemies);
		} else if (Input.GetKeyDown ("4")) {	// Ability 4
			ability4.Execute(this, targetVector, enemies);
		}

		// Activate passives
		ability1.Passive (this, enemies);
		ability2.Passive (this, enemies);
		ability3.Passive (this, enemies);
		ability4.Passive (this, enemies);
	}
}
