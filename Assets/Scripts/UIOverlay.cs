using UnityEngine;
using System.Collections;


[ExecuteInEditMode()]
public class UIOverlay : MonoBehaviour
{
    public Texture sidebars;
    public Texture julietPortrait;
    public Texture julietHealthBarFrame;
    public Texture julietHealthBarFill;
    public Texture patternFrame1;
    public Texture patternFrame2;
    public Texture patternFrame3;

    public float health = 0.75f;

    void OnGUI()
    {
        GUI.matrix = Matrix4x4.TRS( new Vector3( 0, 0, 0 ), Quaternion.identity, new Vector3( Screen.width/1600.0f, Screen.height/900.0f, 1.0f ) );

        GUI.DrawTexture( new Rect( 0, 0, 1600, 900 ), sidebars );
        GUI.DrawTexture( new Rect(35,35,117,114), julietPortrait );
        GUI.DrawTexture( new Rect( 35, 220, 117, 597 ), julietHealthBarFrame );
        GUI.DrawTextureWithTexCoords( new Rect( 35, 220 + (1.0f-health)*597, 117, health* 597 ), julietHealthBarFill, new Rect( 0, 0, 1, health ) );
        GUI.DrawTexture( new Rect( 1320, 30, 0.75f*313, 0.75f * 336 ), patternFrame1 );
        GUI.DrawTexture( new Rect( 1320, 330, 0.75f * 313, 0.75f * 336 ), patternFrame2 );
        GUI.DrawTexture( new Rect( 1320, 630, 0.75f * 313, 0.75f * 336 ), patternFrame3 );
    }
}
