using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    public static int aap = 4;

    public GameObject prefabTileFloor = null;

    public Dancer prefabDancer = null;
    
    public Position2D mapSize = null;
    
    public float tileSize = 0.5f;

    public DancePattern dancePattern;
    
    public float beatInterval = 0.5f;

    public float beatTimer = 0.0f;

    GameObject[,] map = null;

    Dancer dancer = null;

    Position2D dancerPosition;
    
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

        dancerPosition = new Position2D( Mathf.FloorToInt( Random.value * mapSize.i ), Mathf.FloorToInt( Random.value * mapSize.j ) );
        dancer = Instantiate( prefabDancer, new Vector3( dancerPosition.i*tileSize-mapSize.i*tileSize*0.5f,dancerPosition.j*tileSize, -1.0f ), Quaternion.identity ) as Dancer;
        dancer.SetMapPosition( dancerPosition );
        dancer.UpdatePosition( mapSize, tileSize );
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
            dancerPosition += dancePattern.GetCurrentMove();
            dancer.transform.position = new Vector3();
            dancePattern.NextMove();
        }
    }
    
}
