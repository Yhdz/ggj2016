using UnityEngine;


public class Position2D
{
	public int i = 0;
	public int j = 0;

	public Position2D( int i = 0, int j = 0 )
	{
		this.i = i;
		this.j = j;
	}

    public static Position2D operator+( Position2D a, Position2D b )
    {
        return new Position2D( a.i + b.i, a.j + b.j );
    }

	public static bool operator==( Position2D a, Position2D b )
	{
		return a.i == b.i && a.j == b.j;
	}
		
	public static bool operator!=( Position2D a, Position2D b )
	{
		return a.i != b.i || a.j != b.j;
	}
		
	override public bool Equals( object o )
	{
		return this == o;
	}
		
	override public int GetHashCode()
	{
		return base.GetHashCode();
	}

	public static int Distance( Position2D a, Position2D b )
	{
		return Mathf.Abs(b.i-a.i) + Mathf.Abs(b.j-a.j);
	}

	public override string ToString()
	{
		return string.Format("("+i+","+j+")");
	}
}
