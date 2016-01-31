using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenControler : MonoBehaviour 
{
	// for auto continue
	public float screenDuration = 10.0f;

	private float screenTime = 0.0f;

	public string nextScene = null;

	// Use this for initialization
	void Start () 
	{
		if( nextScene == null || nextScene.Length == 0 || Input.GetKeyDown(KeyCode.Space) )
		{
			throw new UnityException( "No next scene selected" );
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		screenTime += Time.deltaTime;

		if( Input.anyKeyDown || screenTime > screenDuration )
		{
			SceneManager.LoadScene( nextScene );
		}
	}
}
