using UnityEngine;

/**
 * Dancer. Follows patterns and animates motion and sprite animation for dancers
 * like Romeo or NPC dancers.
 */
public class Dancer : MonoBehaviour
{
    // The dance pattern the dancer is following
    public DancePattern currentPattern = null;

	public AnimationCurve ErrorCurve = new AnimationCurve ();

    // The sprite animation series the dancer follows
    public Sprite[] spriteAnimation = null;

    // The index of the sprite animation
    int currentSpriteIndex = 0;

	Vector2 position;

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

	private LevelManager levelManager = null;

	private bool useErrorCurve = false;

    void Start()
    {
        // Fetch some of the objects for future use
        GameObject camera = GameObject.Find( "Main Camera" );
		levelManager = camera.GetComponent<LevelManager> ();
		mapTileSize = levelManager.tileSize;
		mapOffset = levelManager.offset;
        sequencer = camera.GetComponent<Sequencer>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Update the start position from current position by matching it to the grid
        int i = Mathf.FloorToInt( (transform.position.x - mapOffset.x) / mapTileSize + 0.5f );
		int j = Mathf.FloorToInt( (transform.position.y - mapOffset.y) / mapTileSize + 0.5f );
		position = new Vector2 (i, j);
		Vector2 scenePosition = mapOffset + mapTileSize * position;

		transitionStartPosition = new Vector3( scenePosition.x, scenePosition.y, transform.position.z );
		transitionEndPosition = transitionStartPosition;
	}

    public void Update()
    {
        // Update position to move to when the beat has passed
        if( sequencer.IsBeatChangeFrame() )
        {
			useErrorCurve = false;

            // Update the start position from current position by matching it to the grid
			Vector2 startScenePosition = mapOffset + mapTileSize * position;
			transitionStartPosition = new Vector3( startScenePosition.x, startScenePosition.y, transform.position.z );

            // Proceed to the next move in the current dance pattern and take it's movement vector
            if( currentPattern != null )
            {
				Vector2 currentMove = currentPattern.GetMove(sequencer.CurrentBeat);
				Vector2 newPosition = position + currentMove;
				Vector2 newScenePosition = mapOffset + mapTileSize * newPosition;

				int mask = 1 << LayerMask.NameToLayer("Collider");
				RaycastHit2D hit = Physics2D.Linecast(startScenePosition, newScenePosition, mask);

				// test if new move is outside of field
				//if (levelManager.IsPositionInField((int)newPosition.x, (int)newPosition.y)) {
				if (hit.collider == null) {
					position = newPosition;
					transitionEndPosition = new Vector3( newScenePosition.x, newScenePosition.y, transitionStartPosition.z );
				}
				else {
					transitionEndPosition = transitionStartPosition;
					transitionEndPosition = new Vector3( newScenePosition.x, newScenePosition.y, transitionStartPosition.z );

                    // decrease Juliette happyiness
                    levelManager.LoseHappiness(this);

					useErrorCurve = true;
				}
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

		

		if (!useErrorCurve) {
			// Use the set animation curve to apply the transition to the next position
			float animationProgress = (currentPattern == null) ? 0.0f : currentPattern.GetCurve (sequencer.CurrentBeat).Evaluate (sequencer.GetBeatPercentage ());
			Vector3 animationState = Vector3.Lerp (transitionStartPosition, transitionEndPosition, animationProgress);
			transform.position = new Vector3 (animationState.x, animationState.y, transitionStartPosition.z);
		} else {
			// Use the set animation curve to apply the transition to the next position
			float animationProgress = ErrorCurve.Evaluate (sequencer.GetBeatPercentage ());
			Vector3 animationState = Vector3.Lerp (transitionStartPosition, transitionEndPosition, animationProgress);
			transform.position = new Vector3 (animationState.x, animationState.y, transitionStartPosition.z);
		}
    }
}
