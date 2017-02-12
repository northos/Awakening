using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float health;
	public float maxHealth;
	public GameObject damageText;

	// apply a given amount of damage to this enemy
	// returns whether or not this kills the enemy
	public bool TakeDamage (float damage){
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

	// set enemy's starting stats
	void Start () {
		health = maxHealth;
	}
	
	// 
	void Update () {
	
	}

	// dim sprite slightly when the player mouses over
	public void OnMouseEnter () {
		GetComponent<SpriteRenderer> ().color = Color.gray;
	}

	// remove the dimming when the player mouses off
	public void OnMouseExit () {
		GetComponent<SpriteRenderer> ().color = Color.white;
	}
}
