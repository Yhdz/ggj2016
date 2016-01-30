using UnityEngine;
using System.Collections;

public class Dancer : MonoBehaviour
{
    public DancePattern currentPattern = null;

    public void Move( float tileSize )
    {
        if( currentPattern == null )
            return;

        Vector2 move = currentPattern.GetCurrentMove();
        transform.Translate( tileSize * move.x, tileSize * move.y, 0.0f );
        currentPattern.NextMove();
    }
}
