using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    public static int aap = 4;

    public GameObject prefabTileFloor = null;

    public Dancer prefabDancer1 = null;

    public Dancer prefabDancer2 = null;

    public Position2D mapSize = null;
    
    public float tileSize = 0.5f;

    public DancePattern dancePattern1;

    public DancePattern dancePattern2;

    public float beatInterval = 0.5f;

    public float beatTimer = 0.0f;

    GameObject[,] map = null;

    Dancer dancer1 = null;

    Dancer dancer2 = null;

    void Start()
    {
        GameObject mapObject = new GameObject( "Map" );
        map = new GameObject[mapSize.i,mapSize.j];
        for( int i = 0; i < mapSize.i; i++ )
            for( int j = 0; j < mapSize.j; j++ )
            {
                map[i, j] = Instantiate( prefabTileFloor, new Vector3( i * tileSize - mapSize.i * tileSize * 0.5f, j * tileSize, 0.0f ), Quaternion.identity ) as GameObject;
                map[i, j].transform.parent = mapObject.transform;
            }

        Position2D dancer1Position = new Position2D( Mathf.FloorToInt( Random.value * mapSize.i ), Mathf.FloorToInt( Random.value * mapSize.j ) );
        dancer1 = Instantiate( prefabDancer1, new Vector3( dancer1Position.i*tileSize-mapSize.i*tileSize*0.5f,dancer1Position.j*tileSize, -1.0f ), Quaternion.identity ) as Dancer;
        dancer1.SetMapPosition( dancer1Position );
        dancer1.UpdatePosition( mapSize, tileSize );

        Position2D dancer2Position = new Position2D( Mathf.FloorToInt( Random.value * mapSize.i ), Mathf.FloorToInt( Random.value * mapSize.j ) );
        dancer2 = Instantiate( prefabDancer2, new Vector3( dancer2Position.i * tileSize - mapSize.i * tileSize * 0.5f, dancer2Position.j * tileSize, -1.0f ), Quaternion.identity ) as Dancer;
        dancer2.SetMapPosition( dancer2Position );
        dancer2.UpdatePosition( mapSize, tileSize );

    }

    void Update()
    {
        // Update beat timer
        beatTimer += Time.deltaTime;
        if ( beatTimer > beatInterval )
        {
            // Update beat
            beatTimer -= beatInterval;

            // Move one step of the pattern
            dancer1.DoMove( dancePattern1.GetCurrentMove() );
            dancePattern1.NextMove();
            dancer1.UpdatePosition( mapSize, tileSize );

            // Move one step of the pattern
            dancer2.DoMove( dancePattern2.GetCurrentMove() );
            dancePattern2.NextMove();
            dancer2.UpdatePosition( mapSize, tileSize );
        }
    }
    
}
