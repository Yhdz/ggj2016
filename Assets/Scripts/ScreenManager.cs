using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Uween;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 	Screen manager.
/// Fades UI elements stored in the screens List.
/// UI element can be Text, Image or ...
/// Uses UTween: https://github.com/oinkgms/uween
/// 
/// </summary>
public class ScreenManager : MonoBehaviour 
{
	// container for references to the screens
	public List<GameObject> screens = new List<GameObject>(2);

	int currentScreenIndex = 0;

	public float fadingDuration = 1.0f;
	public float fadingDelay = 2.0f;
	public bool autoFade = false;
	bool fading = false;

	// Use this for initialization
	void Start () 
	{
		HideAllButFirst();

		if( autoFade )
			FadeOut();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( Input.GetKeyDown( KeyCode.RightArrow ) && !fading )
		{
			FadeOut();
		}
	}

	/// <summary>
	/// 	Fades the screen out.
	/// 	(delay is NOT the delay _after_ the tween but _before_ the tween)
	/// </summary>
	void FadeOut( )
	{
		TweenA.Add( screens[currentScreenIndex], fadingDuration, 0.0f ).Delay( fadingDelay ).Then( FadeInNextScreen );
	}

	/// <summary>
	/// 	Fades the in next screen.
	/// 	If there is no more screens left, loads the next scene
	/// </summary>
	void FadeInNextScreen()
	{
		currentScreenIndex++;

		if( currentScreenIndex >= screens.Count )
		{
			SceneManager.LoadScene( (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings );
			return;
		}

		fading = true;
		TweenA.Add( screens[currentScreenIndex], fadingDuration, 1.0f ).Then( FadeDone );
	}

	/// <summary>
	/// When one 'screen' has faded in, this starts fading out. fadingDelay determines the delay for the starting of the fade
	/// </summary>
	void FadeDone()
	{
		fading = false;
		if( autoFade )
			FadeOut();
	}

	/// <summary>
	/// 	Sets the sprites'/gameobjects' initial transparency.
	/// </summary>
	void HideAllButFirst()
	{
		foreach (var screen in screens)
		{
			Color cw;

			Graphic g = screen.GetComponent<Graphic>();

			cw = g.color;

			if( screen == screens[currentScreenIndex] )
				cw.a = 1;
			else
				cw.a = 0;

			g.color = cw;
		}
	}
}
