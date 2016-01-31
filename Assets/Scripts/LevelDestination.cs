using UnityEngine;
using System.Collections;

public class LevelDestination : MonoBehaviour {
	public Color BlinkColor;

	private LevelManager levelManager = null;
	private Sequencer sequencer = null;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
		sequencer = FindObjectOfType<Sequencer> ();
	}
	
	// Update is called once per frame
	void Update () {
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();

		float colorLerp = Mathf.Sin (Time.time * 2 * Mathf.PI * 1f) * 0.5f + 0.5f;
		spriteRenderer.color = Color.Lerp (Color.white, BlinkColor, colorLerp);
	}

	void OnTriggerStay2D(Collider2D other) {
		// Test if sequencer is around the end or start of a beat (unfortunately the beat detection doesnt seem to work inside the collider)
		if (Mathf.Abs(sequencer.beatPercentage - 1.0f) < 0.1f) {
			if (levelManager != null) {
				levelManager.OnDestinationReached ();
			}
		}
	}
}
