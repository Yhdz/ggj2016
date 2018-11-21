using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages the level: the tiles, the dancers, the beat, everything.
 */
public class LevelManager : MonoBehaviour
{
    public enum BadEffectType
    {
        HappinessDec,
    };

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

	public float JulietteHappiness = 1.0f;

    public float julietteHappinessChunk = 0.2f;

    // The dancers in the level
    Dancer[] dancers = null;

    // The index of the current pattern playing
    public int currentPatternIndex = 0;

    // The index of the next pattern to select
    public int nextPatternIndex = -1;

    // Link to the sequencer
<<<<<<< HEAD
    private Sequencer sequencer = null;

	private UIOverlay uiOverlay = null;

    // Z-sort list
    SortForZOrder[] sortZOrderObjects = null;

    public BadEffectType badEffect = BadEffectType.HappinessDec;
=======
    Sequencer sequencer = null;
>>>>>>> parent of 04b37f8... Merge branch 'develop' of https://github.com/Yhdz/ggj2016 into develop

    /**
     * Starts the level.
     */
    void Start()
    {
        // Fetch sequencer
        sequencer = GetComponent<Sequencer>();
<<<<<<< HEAD
		uiOverlay = GameObject.FindObjectOfType<UIOverlay>();
		uiOverlay.fadeoutValue = 0.0f;
		uiOverlay.StartFadeIn (0.5f);
=======
>>>>>>> parent of 04b37f8... Merge branch 'develop' of https://github.com/Yhdz/ggj2016 into develop

        // Collect all dancers
        dancers = GameObject.FindObjectsOfType<Dancer>();
        if( dancers == null )
            Debug.Log( "Warning: no dancers defined in scene!" );

        // Fetch all objects marked with z-order script
        sortZOrderObjects = GameObject.FindObjectsOfType<SortForZOrder>();

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
<<<<<<< HEAD

        // Update Z-order
        System.Array.Sort( sortZOrderObjects, new YPositionSorter() );
        for( int i = 0; i < sortZOrderObjects.Length; i++ )
            sortZOrderObjects[i].GetComponent<SpriteRenderer>().sortingOrder = (i + 1);

        uiOverlay.health = Mathf.Clamp01(JulietteHappiness);
    }

    public void BadStuffHappened( Dancer dancer )
    {
        if( dancer == dancerRomeo && badEffect == BadEffectType.HappinessDec )
        {
            JulietteHappiness -= julietteHappinessChunk;
            if ( JulietteHappiness <= 0.0f )
                StartCoroutine( EndGameSequence() );
        }
=======
>>>>>>> parent of 04b37f8... Merge branch 'develop' of https://github.com/Yhdz/ggj2016 into develop
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
		uiOverlay.StartFadeIn (-2.0f);

		yield return new WaitForSeconds (2.0f);

        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
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
<<<<<<< HEAD

public class YPositionSorter : IComparer
{
    int IComparer.Compare( System.Object x, System.Object y )
    {
        GameObject a = ((Component)x).gameObject;
        GameObject b = ((Component)y).gameObject;

        if ( a.transform.position.y != b.transform.position.y )
            return (int)(b.transform.position.y - a.transform.position.y);
        else
            return (int)(b.transform.position.x - a.transform.position.x);
    }

}
=======
>>>>>>> parent of 04b37f8... Merge branch 'develop' of https://github.com/Yhdz/ggj2016 into develop
