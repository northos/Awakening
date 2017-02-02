using UnityEngine;
using System.Collections;

public class GlowFlicker : MonoBehaviour {
	Light glow;
	public float minGlow = 0.2f;
	public float maxGlow = 0.4f;
	public float glowDelta = 0.01f;

	// Pick out the light component
	void Start () {
		glow = gameObject.GetComponent<Light> ();
	}
	
	// Randomly dim or brighten the glow, but never pass the minimum or maximum
	void Update () {
		float delta = Random.Range(-glowDelta, glowDelta);
		glow.intensity += delta;
		if (glow.intensity < minGlow) {
			glow.intensity = minGlow;
		} else if (glow.intensity > maxGlow) {
			glow.intensity = maxGlow;
		}
	}
}
