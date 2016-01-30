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
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (levelManager != null) {
			levelManager.OnDestinationReached ();
		}
	}
}
