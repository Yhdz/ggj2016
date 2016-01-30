using UnityEngine;

/**
 * Dancer. Follows patterns and animates motion and sprite animation for dancers
 * like Romeo or NPC dancers.
 */
public class Dancer : MonoBehaviour
{
    // The dance pattern the dancer is following
    public DancePattern currentPattern = null;

    // The sprite animation series the dancer follows
    public Sprite[] spriteAnimation = null;

    // The index of the sprite animation
    int currentSpriteIndex = 0;

    // Start position of the current transition
    Vector3 transitionStartPosition;

    // End position of the current transition
    Vector3 transitionEndPosition;
    
    // Tile size taken from the level manager
    float mapTileSize;

    // Map offset taken from the level manager
    Vector2 mapOffset;

    // The sequencer
    Sequencer sequencer = null;

    // The sprite renderer
    SpriteRenderer spriteRenderer = null;

    void Start()
    {
        // Fetch some of the objects for future use
        GameObject camera = GameObject.Find( "Main Camera" );
        mapTileSize = camera.GetComponent<LevelManager>().tileSize;
        mapOffset = camera.GetComponent<LevelManager>().offset;
        sequencer = camera.GetComponent<Sequencer>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Update the start position from current position by matching it to the grid
        int i = Mathf.FloorToInt( (transform.position.x - mapOffset.x) / mapTileSize + 0.5f );
        int j = Mathf.FloorToInt( (transform.position.y - mapOffset.y) / mapTileSize + 0.5f );
        transitionStartPosition = new Vector3( mapOffset.x + mapTileSize * i, mapOffset.y + mapTileSize * j, transform.position.z );
		transitionEndPosition = transitionStartPosition;
    }

    public void Update()
    {
        // Update position to move to when the beat has passed
        if( sequencer.IsBeatChangeFrame() )
        {
            // Update the start position from current position by matching it to the grid
            int i = Mathf.FloorToInt( (transform.position.x - mapOffset.x) / mapTileSize + 0.5f );
            int j = Mathf.FloorToInt( (transform.position.y - mapOffset.y) / mapTileSize + 0.5f );
            transitionStartPosition = new Vector3( mapOffset.x + mapTileSize * i, mapOffset.y + mapTileSize * j, transform.position.z );

            // Proceed to the next move in the current dance pattern and take it's movement vector
            if( currentPattern != null )
            {
				Vector2 currentMove = mapTileSize * currentPattern.GetMove(sequencer.CurrentBeat);
                transitionEndPosition = transitionStartPosition + new Vector3( currentMove.x, currentMove.y, transitionStartPosition.z );
            }
            else
            {
                transitionEndPosition = transitionStartPosition;
            }

            // Update sprite animation if available
            if( spriteAnimation != null )
            {
                currentSpriteIndex = (currentSpriteIndex + 1) % spriteAnimation.Length;
                spriteRenderer.sprite = spriteAnimation[currentSpriteIndex];
            }
        }

        // Use the set animation curve to apply the transition to the next position
		float animationProgress = (currentPattern == null) ? 0.0f : currentPattern.GetCurve(sequencer.CurrentBeat).Evaluate( sequencer.GetBeatPercentage() );
        Vector3 animationState = Vector3.Lerp( transitionStartPosition, transitionEndPosition, animationProgress );
        transform.position = new Vector3( animationState.x, animationState.y, transitionStartPosition.z );
    }
}
