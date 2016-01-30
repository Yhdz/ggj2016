using UnityEngine;
using System.Collections;

public class Dancer : MonoBehaviour
{
    public DancePattern currentPattern = null;

    Vector3 startPosition;

    Vector3 movement;

    float tileSize;

    Vector2 offset;

    Sequencer sequencer = null;

    void Start()
    {
        GameObject camera = GameObject.Find( "Main Camera" );
        tileSize = camera.GetComponent<LevelManager>().tileSize;
        offset = camera.GetComponent<LevelManager>().offset;
        sequencer = camera.GetComponent<Sequencer>();

        int i = Mathf.FloorToInt( (transform.position.x - offset.x) / tileSize + 0.5f );
        int j = Mathf.FloorToInt( (transform.position.y - offset.y) / tileSize + 0.5f );
        startPosition = new Vector3( offset.x + tileSize * i, offset.y + tileSize * j, transform.position.z );
    }

    public void Update()
    {
        if( currentPattern == null )
            return;

        if( sequencer.IsBeatChangeFrame() )
        {
            // TODO: startPosition = rounded current position
            int i = Mathf.FloorToInt( (transform.position.x - offset.x) / tileSize + 0.5f );
            int j = Mathf.FloorToInt( (transform.position.y - offset.y) / tileSize + 0.5f );
            startPosition = new Vector3( offset.x + tileSize * i, offset.y + tileSize * j, transform.position.z );

            currentPattern.NextMove();
            movement = tileSize * currentPattern.GetCurrentMove();
            
        }

        AnimationCurve animationCurve = currentPattern.GetCurrentCurve();
        float animationPosition = animationCurve.Evaluate( sequencer.GetBeatPercentage() );
        Debug.Log( animationPosition );
        transform.position = Vector3.Lerp( startPosition, startPosition + movement, animationPosition );
             

    }

    
}
