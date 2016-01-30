using UnityEngine;
using System.Collections;

[System.Serializable]
public class DancePattern : MonoBehaviour
{
    public Vector2[] moves;

    public AnimationCurve[] curves;

	public Vector2 GetMove(int beat)
    {
		return moves[beat];
    }

	public AnimationCurve GetCurve(int beat)
    {
        return curves[beat];
    }
}
