using UnityEngine;
using System.Collections;

public class Dancer : MonoBehaviour
{
    DancePattern currentPattern;

    public void Move()
    {
        Vector2 position = new Vector2( transform.position.x, transform.position.y );
        position += currentPattern.GetCurrentMove();
        currentPattern.NextMove();
    }
}
