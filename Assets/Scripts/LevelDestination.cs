using UnityEngine;
using System.Collections;

public class LevelDestination : MonoBehaviour {
	private LevelManager levelManager = null;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();

		float alphaColor = Mathf.Sin (Time.time * 2 * Mathf.PI * 1f) * 0.5f + 0.5f;
		spriteRenderer.color = new Color (spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alphaColor);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (levelManager != null) {
			levelManager.OnDestinationReached ();
		}
	}
}
