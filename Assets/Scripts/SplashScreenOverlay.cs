using UnityEngine;
using System.Collections;

public class SplashScreenOverlay : MonoBehaviour
{
	public Texture splashImage;

	LevelManager levelManager;

	void Start()
	{
	}

	void OnGUI()
	{
		GUI.matrix = Matrix4x4.TRS( new Vector3( 0, 0, 0 ), Quaternion.identity, new Vector3( Screen.width/1600.0f, Screen.height/900.0f, 1.0f ) );

		GUI.DrawTexture( new Rect( 400, 150, 800, 600 ), splashImage );
	}
}
