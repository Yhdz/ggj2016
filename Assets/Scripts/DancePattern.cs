using UnityEngine;
using System.Collections;

[System.Serializable]
public class DancePattern : MonoBehaviour
{
    public Vector2[] moves;

    public AnimationCurve[] curves;

    public int currentIndex = -1;

    public int previousIndex = -1;

    public void Update()
    {
        previousIndex = currentIndex;
    }

    public Vector2 GetCurrentMove()
    {
        return moves[currentIndex];
    }

    public AnimationCurve GetCurrentCurve()
    {
        return curves[currentIndex];
    }

    public void NextMove()
    {
        currentIndex = (currentIndex + 1) % moves.Length;
    }
}
