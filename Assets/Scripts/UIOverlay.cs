using UnityEngine;
using System.Collections;

public class UIOverlay : MonoBehaviour
{
    public Texture sidebars;

    public Color colorCurrentPattern = new Color( 0, 0.05f, 0, 0.5f );
    public Color colorNextPattern = new Color( 0, 0.2f, 0, 0.5f );

    public Texture julietPortrait;
    public Texture julietHealthBarFrame;
    public Texture julietHealthBarFill;

    public Texture patternFrame1;
    public Texture patternFrame2;
    public Texture patternFrame3;

    public Texture patternIcon1;
    public Texture patternIcon2;
    public Texture patternIcon3;

	public Texture fadeoutTexture;
	public float fadeoutValue = 1.0f;

    public float health = 0.75f;

	public bool doDrawHealthBar = true;

    LevelManager levelManager;

    void Start()
    {
        levelManager = GameObject.Find("Main Camera").GetComponent<LevelManager>();
    }

    void OnGUI()
    {
        GUI.matrix = Matrix4x4.TRS( new Vector3( 0, 0, 0 ), Quaternion.identity, new Vector3( Screen.width/1600.0f, Screen.height/900.0f, 1.0f ) );

        GUI.DrawTexture( new Rect( 0, 0, 1600, 900 ), sidebars );

		if( doDrawHealthBar )
		{
	        GUI.DrawTexture( new Rect(35,35,117,114), julietPortrait );
	        GUI.DrawTexture( new Rect( 35, 220, 117, 597 ), julietHealthBarFrame );
	        GUI.DrawTextureWithTexCoords( new Rect( 35, 220 + (1.0f-health)*597, 117, health* 597 ), julietHealthBarFill, new Rect( 0, 0, 1, health ) );
		}

        int cpi = levelManager.currentPatternIndex;
        int npi = levelManager.nextPatternIndex;

        GUI.color = (cpi == 0 ? colorCurrentPattern : (npi == 0 ? colorNextPattern : Color.white));
        GUI.DrawTexture( new Rect( 1320, 30, 0.75f*313, 0.75f * 336 ), patternFrame1 );

        GUI.color = (cpi == 1 ? colorCurrentPattern : (npi == 1 ? colorNextPattern : Color.white));
        GUI.DrawTexture( new Rect( 1320, 330, 0.75f * 313, 0.75f * 336 ), patternFrame2 );

        GUI.color = (cpi == 2 ? colorCurrentPattern : (npi == 2 ? colorNextPattern : Color.white));
        GUI.DrawTexture( new Rect( 1320, 630, 0.75f * 313, 0.75f * 336 ), patternFrame3 );

        GUI.color = Color.white;

        GUI.DrawTexture( new Rect( 1340, 35, 0.75f * 313, 0.75f * 336 ), patternIcon1 );
        GUI.DrawTexture( new Rect( 1340, 335, 0.75f * 313, 0.75f * 336 ), patternIcon2 );
        GUI.DrawTexture( new Rect( 1340, 635, 0.75f * 313, 0.75f * 336 ), patternIcon3 );

		GUI.color = Color.black * (1.0f - fadeoutValue);
		GUI.DrawTexture (new Rect( 0, 0, 1600, 900 ), fadeoutTexture);
    }

	public void StartFadeIn(float duration)
	{
		StartCoroutine(FadeCoRoutine(1.0f / duration));
	}

	public IEnumerator FadeCoRoutine(float speed)
	{
		while (fadeoutValue >= 0.0f && fadeoutValue <= 1.0f)
		{
			fadeoutValue += Time.deltaTime * speed;
			yield return null;
		}
		fadeoutValue = Mathf.Clamp01 (fadeoutValue);
	}
}

