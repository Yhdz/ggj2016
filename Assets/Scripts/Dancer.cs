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
    }

    public void Update()
    {
        // Only update when a dance pattern is available
        if( currentPattern == null )
            return;
        
        // Update position to move to when the beat has passed
        if( sequencer.IsBeatChangeFrame() )
        {
            // Update the start position from current position by matching it to the grid
            int i = Mathf.FloorToInt( (transform.position.x - mapOffset.x) / mapTileSize + 0.5f );
            int j = Mathf.FloorToInt( (transform.position.y - mapOffset.y) / mapTileSize + 0.5f );
            transitionStartPosition = new Vector3( mapOffset.x + mapTileSize * i, mapOffset.y + mapTileSize * j, transform.position.z );

            // Proceed to the next move in the current dance pattern and take it's movement vector
            currentPattern.NextMove();
            Vector2 currentMove = mapTileSize * currentPattern.GetCurrentMove();
            transitionEndPosition = transitionStartPosition + new Vector3(currentMove.x,currentMove.y,0.0f);

            // Update sprite animation if available
            if( spriteAnimation != null )
            {
                currentSpriteIndex = (currentSpriteIndex + 1) % spriteAnimation.Length;
                spriteRenderer.sprite = spriteAnimation[currentSpriteIndex];
            }
        }

        // Use the set animation curve to apply the transition to the next position
        transform.position = Vector3.Lerp( transitionStartPosition, transitionEndPosition, 
            currentPattern.GetCurrentCurve().Evaluate( sequencer.GetBeatPercentage() ) );
    }
}
