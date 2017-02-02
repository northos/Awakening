using UnityEngine;
using System.Collections;

public class DamageText : MonoBehaviour {
	public float lifetime;
	public float riseSpeed;
	float duration = 0f;
	
	// slowly raise the text for a short time, then destroy it
	void Update () {
		duration += Time.deltaTime;
		transform.Translate(new Vector3(0f, riseSpeed * Time.deltaTime, 0f));
		if (duration >= lifetime)
			Destroy (gameObject);
	}
}
