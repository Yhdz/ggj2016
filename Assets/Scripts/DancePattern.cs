using UnityEngine;
using System.Collections;

[System.Serializable]
public class DancePattern : MonoBehaviour
{
    public int maxMoves = 3;

    public Vector2[] moves;

    public AnimationCurve[] curves;

	public Vector2 GetMove(int beat)
    {
        Debug.Log( beat + " of " + maxMoves + "/"+moves.Length);
		return moves[beat%maxMoves];
    }

	public AnimationCurve GetCurve(int beat)
    {
        return curves[beat%maxMoves];
    }
}
