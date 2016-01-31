using UnityEngine;
using System.Collections;

public class SpriteAnimator : MonoBehaviour
{
    // The sprite animation series the dancer follows
    public Sprite[] spriteAnimation = null;

    // The index of the sprite animation
    int currentSpriteIndex = 0;

    // The sequencer
    protected Sequencer sequencer = null;

    // The sprite renderer
    protected SpriteRenderer spriteRenderer = null;

    float spriteAnimationTimer = 0.0f;

    public float spriteAnimationTimeOut = 0.0f;

    // Use this for initialization
    void Start ()
    {
        GameObject camera = GameObject.Find( "Main Camera" );
        sequencer = camera.GetComponent<Sequencer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if( spriteAnimation != null )
        {
            if( spriteAnimationTimeOut == 0.0f )
            {
                if( sequencer.IsBeatChangeFrame() )
                {
                    // Update sprite animation if available
                    currentSpriteIndex = (currentSpriteIndex + 1) % spriteAnimation.Length;
                    spriteRenderer.sprite = spriteAnimation[currentSpriteIndex];
                }
            }
            else
            {
                spriteAnimationTimer += Time.deltaTime;
                if ( spriteAnimationTimer > spriteAnimationTimeOut )
                {
                    spriteAnimationTimer -= spriteAnimationTimeOut;

                    currentSpriteIndex = (currentSpriteIndex + 1) % spriteAnimation.Length;
                    spriteRenderer.sprite = spriteAnimation[currentSpriteIndex];
                }
            }
        }
    }
}
