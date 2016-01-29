using UnityEngine;
using System.Collections;

[System.Serializable]
public class DancePattern
{
    [System.Serializable]
    public enum DanceMove
    {
        Left,
        Right,
        Up,
        Down,
    };

    public DanceMove[] pattern;

    public int currentIndex = 0;

    public DanceMove GetCurrentMove()
    {
        return pattern[currentIndex];
    }

    public void NextMove()
    {
        currentIndex = (currentIndex + 1) % pattern.Length;
    }
}
