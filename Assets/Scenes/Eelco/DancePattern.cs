using UnityEngine;
using System.Collections;

[System.Serializable]
public class DancePattern
{
    public Position2D[] pattern;

    public int currentIndex = 0;

    public Position2D GetCurrentMove()
    {
        return pattern[currentIndex];
    }

    public void NextMove()
    {
        currentIndex = (currentIndex + 1) % pattern.Length;
    }
}
