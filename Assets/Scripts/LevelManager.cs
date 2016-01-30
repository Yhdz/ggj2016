using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages the level: the tiles, the dancers, the beat, everything.
 */
public class LevelManager : MonoBehaviour
{
    // The size of one tile in world units
    public float tileSize = 36.0f;

    // The offset of the tiles
    public Vector2 offset = new Vector2();

    // The map width
    public int mapWidth = 6;

    // The map height
    public int mapHeight = 6;
    
    // The slots of the patterns for this level (1-3)
    public DancePattern[] patternSlots = null;

    //  The dancer that represents Romeo
    public Dancer dancerRomeo = null;

    // The map of floor tiles
    FloorTile[,] floorMap = null;

    // The dancers in the level
    Dancer[] dancers = null;

    // The index of the current pattern playing
    public int currentPatternIndex = 0;

    // The index of the next pattern to select
    public int nextPatternIndex = -1;

    // Link to the sequencer
    Sequencer sequencer = null;

    /**
     * Starts the level.
     */
    void Start()
    {
        // Fetch sequencer
        sequencer = GetComponent<Sequencer>();

        // Collect all tiles in the scene and map them onto the map structure
        FloorTile[] floorTiles = GameObject.FindObjectsOfType<FloorTile>();
        floorMap = new FloorTile[mapWidth,mapHeight];
        foreach( FloorTile floorTile in floorTiles )
        {
            int i = Mathf.FloorToInt( (floorTile.transform.position.x - offset.x) / tileSize + 0.5f );
            int j = Mathf.FloorToInt( (floorTile.transform.position.y - offset.y) / tileSize + 0.5f );

            floorMap[i, j] = floorTile;
        }

        // Collect all dancers
        dancers = GameObject.FindObjectsOfType<Dancer>();
        if( dancers == null )
            Debug.Log( "Warning: no dancers defined in scene!" );

        // Get pattern from current slot and apply to romeo
        dancerRomeo.currentPattern = patternSlots[0];

    }

    void Update()
    {
        // Process input
        if( Input.GetKeyDown( KeyCode.A ) )
            nextPatternIndex = 0;
        if( Input.GetKeyDown( KeyCode.S ) )
            nextPatternIndex = 1;
        if( Input.GetKeyDown( KeyCode.D ) )
            nextPatternIndex = 2;

        // Update UI highlighting
        for( int i = 0; i < patternSlots.Length; i++ )
        {
            /*SpriteRenderer spriteRenderer = patternSlots[i].GetComponent<SpriteRenderer>();
            if( i == currentPatternIndex )
                spriteRenderer.color = Color.green;
            else if( i == nextPatternIndex )
                spriteRenderer.color = Color.red;
            else
                spriteRenderer.color = Color.white;*/
        }


        // Move to next pattern if a different one is selected
        if( sequencer.IsMeasureChangeFrame() && nextPatternIndex != -1 )
        {
            currentPatternIndex = nextPatternIndex;

            dancerRomeo.currentPattern = patternSlots[currentPatternIndex];
        }
    }

	public void OnDestinationReached()
	{
		StartCoroutine(EndGameSequence());
	}

	private IEnumerator EndGameSequence()
	{
		while (!sequencer.IsBeatChangeFrame ()) {
			yield return null;
		}

		sequencer.StopSequencer ();

		yield return new WaitForSeconds (2.0f);

		UnityEngine.SceneManagement.SceneManager.LoadScene ("DanceLevel");
	}

	public bool IsPositionInField(int x, int y)
	{
		if (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight) {
			return true;
		} else {
			return false;
		}
	}
}
