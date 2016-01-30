using UnityEngine;
using System.Collections;

[System.Serializable]
public class DancePattern
{
    public Vector2[] pattern;

    public int currentIndex = 0;

    public Vector2 GetCurrentMove()
    {
        return pattern[currentIndex];
    }

    public void NextMove()
    {
        currentIndex = (currentIndex + 1) % pattern.Length;
    }
}
