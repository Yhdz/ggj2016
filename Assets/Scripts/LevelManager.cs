using UnityEngine;
using System.Collections;

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

	public float JulietteHappiness = 0.5f;

    // The dancers in the level
    Dancer[] dancers = null;

    // The index of the current pattern playing
    public int currentPatternIndex = 0;

    // The index of the next pattern to select
    public int nextPatternIndex = -1;

    // Link to the sequencer
    Sequencer sequencer = null;

    // Z-sort list
    public GameObject[] sortZOrderObjects = null;

    /**
     * Starts the level.
     */
    void Start()
    {
        // Fetch sequencer
        sequencer = GetComponent<Sequencer>();

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

        // Move to next pattern if a different one is selected
        if( sequencer.IsMeasureChangeFrame() && nextPatternIndex != -1 )
        {
            currentPatternIndex = nextPatternIndex;
            dancerRomeo.currentPattern = patternSlots[currentPatternIndex];
        }

        // Update Z-order
        System.Array.Sort( sortZOrderObjects, new YPositionSorter() );
        for( int i = 0; i < sortZOrderObjects.Length; i++ )
            sortZOrderObjects[i].GetComponent<SpriteRenderer>().sortingOrder = (i + 1);
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

public class YPositionSorter : IComparer
{
    int IComparer.Compare( System.Object x, System.Object y )
    {
        GameObject a = (GameObject)x;
        GameObject b = (GameObject)y;

        if ( a.transform.position.y != b.transform.position.y )
            return (int)(b.transform.position.y - a.transform.position.y);
        else
            return (int)(b.transform.position.x - a.transform.position.x);
    }

}