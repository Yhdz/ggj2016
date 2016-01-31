using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSkipResetCheats : MonoBehaviour
{
	void Update()
    {
	    if ( Input.GetKeyDown( KeyCode.F2 ) )
            SceneManager.LoadScene( (SceneManager.GetActiveScene().buildIndex - 1 + SceneManager.sceneCountInBuildSettings) % SceneManager.sceneCountInBuildSettings );
        if( Input.GetKeyDown( KeyCode.F3 ) )
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
        if( Input.GetKeyDown( KeyCode.F4 ) )
            SceneManager.LoadScene( (SceneManager.GetActiveScene().buildIndex + 1 + SceneManager.sceneCountInBuildSettings) % SceneManager.sceneCountInBuildSettings );
        if( Input.GetKeyDown( KeyCode.F5 ) )
            SceneManager.LoadScene( 0 );
        if( Input.GetKeyDown( KeyCode.F6 ) )
            Application.CaptureScreenshot( "screenshot"+Mathf.FloorToInt(Time.timeSinceLevelLoad)+".png" );

    }
}
