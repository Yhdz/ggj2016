[System.Serializable]
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
        return new Position2D( b.i + a.i, b.j + a.j );
    }
}