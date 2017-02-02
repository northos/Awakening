using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	Enemy enemy;
	float maxScale;
	public float maxPos;

	// on start, store some values in local variables for ease of access
	void Start() {
		enemy = GetComponentInParent<Enemy> ();
		maxScale = transform.localScale.x;
		maxPos = -maxScale / 100f;
	}

	// keep this object updated so that it is constantly scaled down appropriately with the enemy's loss of health
	void Update () {
		float scale = enemy.health / enemy.maxHealth;
		transform.localScale = new Vector3 (maxScale * scale, transform.localScale.y, transform.localScale.z);
		transform.localPosition = new Vector3 (maxPos - maxPos * scale, transform.localPosition.y, transform.localPosition.z);
	}
}
