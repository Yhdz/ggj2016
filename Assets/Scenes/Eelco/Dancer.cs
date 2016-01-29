using UnityEngine;
using System.Collections;

public class Dancer : MonoBehaviour
{
    Position2D currentPosition = new Position2D(0,0);
    
    public void DoMove( DancePattern.DanceMove danceMove )
    {
        if( danceMove == DancePattern.DanceMove.Left )
            currentPosition.i = currentPosition.i - 1;
        else if( danceMove == DancePattern.DanceMove.Right )
            currentPosition.i = currentPosition.i + 1;
        else if( danceMove == DancePattern.DanceMove.Up )
            currentPosition.j = currentPosition.j + 1;
        else if( danceMove == DancePattern.DanceMove.Down )
            currentPosition.j = currentPosition.j - 1;
    }

    public void SetMapPosition( Position2D mapPosition )
    {
        currentPosition = mapPosition;
    }

    public void UpdatePosition( Position2D mapSize, float tileSize)
    {
        currentPosition.i = Mathf.Max( 0, Mathf.Min( mapSize.i - 1, currentPosition.i ) );
        currentPosition.j = Mathf.Max( 0, Mathf.Min( mapSize.j - 1, currentPosition.j ) );

        transform.position = new Vector3( currentPosition.i * tileSize - mapSize.i * tileSize * 0.5f, currentPosition.j * tileSize, -1.0f );

    }
}
