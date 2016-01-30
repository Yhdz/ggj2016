using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 	UI for sequences.
/// 	@author Manno
/// </summary>
public class DanceInput : MonoBehaviour 
{
	private List<Position2D> SEQUENCE_ONE = new List<Position2D>(){new Position2D(-1, 0), new Position2D(0, 1), new Position2D(-1, 0)};
	private List<Position2D> SEQUENCE_TWO = new List<Position2D>(){new Position2D(1, 0), new Position2D(0, -1), new Position2D(1, 0)};
	private List<Position2D> SEQUENCE_THREE = new List<Position2D>(){new Position2D(0, 1), new Position2D(-1, 0), new Position2D(0, -1)};

	private List< List<Position2D>> sequences;

	// the UI elements to hilight
	public GameObject SequenceOneGameObject;
	public GameObject SequenceTwoGameObject;
	public GameObject SequenceThreeGameObject;

	public GameObject bar;

	int currentSequence;
	int nextSequence = -1;

	float sequenceDuration = 2.0f;
	float sequenceTime = 0.0f;

	// Use this for initialization
	void Start () 
	{
		// ?!
		sequences = new List<List<Position2D>>();
		sequences.Add( null );
		sequences.Add( null );
		sequences.Add( null );

		LoadSequences( SEQUENCE_ONE, SEQUENCE_TWO, SEQUENCE_THREE );

		currentSequence = 0;
		nextSequence = currentSequence;

		sequenceTime = 0.0f;
	}

	// Update is called once per frame
	void Update () 
	{
		checkInput();

		sequenceTime += Time.deltaTime;
		if( nextSequence != -1 && sequenceTime > sequenceDuration )
		{
			switchSequence();
			sequenceTime -= sequenceDuration;
		}

		updateTimeBar();

		colorUI();
	}

	/// <summary>
	/// 	Updates the time bar to provide feedback to user about timing of switch.
	/// </summary>
	void updateTimeBar()
	{
		bar.transform.localScale = new Vector3( sequenceTime / sequenceDuration, 1, 1 );
	}

	/// <summary>
	/// 	Loads a set of sequences to use.
	/// </summary>
	/// <param name="first">a List Position2D.</param>
	/// <param name="second">List Position2D</param>
	/// <param name="third">List Position2D</param>
	public void LoadSequences( List<Position2D> first, List<Position2D> second, List<Position2D> third )
	{
		sequences[0] = first;
		sequences[1] = second;
		sequences[2] = second;
	}

	/// <summary>
	/// 	Gets the sequence to execute.
	/// </summary>
	/// <returns>The sequence.</returns>
	public List<Position2D> getSequence()
	{
		return sequences[currentSequence];
	}

    public int GetCurrentSequenceIndex()
    {
        return currentSequence;
    }

	/// <summary>
	/// 	Checks the input to set the next sequence in the 'queue'
	/// </summary>
	void checkInput()
	{
		if( Input.GetKeyDown( KeyCode.A ) )
		{
			nextSequence = 0;
		}

		if( Input.GetKeyDown( KeyCode.S ) )
		{
			nextSequence = 1;
		}

		if( Input.GetKeyDown( KeyCode.D ) )
		{
			nextSequence = 2;
		}
	}

	/// <summary>
	/// 	Switchs the currentSequence to be the one waiting in the 'queue'
	/// </summary>
	void switchSequence()
	{
		currentSequence = nextSequence;
		//nextSequence = -1;
	}

	/// <summary>
	/// 	Apply coloring to the UI indicating the sequence 'queue'
	/// </summary>
	void colorUI()
	{
		resetUI();

		// highlight whicever is the next sequence to run
		if (nextSequence == 0 )
		{
			SequenceOneGameObject.GetComponent<SpriteRenderer>().color = Color.red;
		}
		else if ( nextSequence == 1 )
		{
			SequenceTwoGameObject.GetComponent<SpriteRenderer>().color = Color.red;
		}
		else if ( nextSequence == 2 )
		{
			SequenceThreeGameObject.GetComponent<SpriteRenderer>().color = Color.red;
		}

		// higlight which ever is the current running sequence. Yes, overwrites previous coloring
		if (currentSequence == 0 )
		{
			SequenceOneGameObject.GetComponent<SpriteRenderer>().color = Color.green;
		}
		else if ( currentSequence == 1 )
		{
			SequenceTwoGameObject.GetComponent<SpriteRenderer>().color = Color.green;
		}
		else if ( currentSequence == 2 )
		{
			SequenceThreeGameObject.GetComponent<SpriteRenderer>().color = Color.green;
		}
	}

	/// <summary>
	/// 	Resets the UI colors
	/// </summary>
	void resetUI()
	{
		SequenceOneGameObject.GetComponent<SpriteRenderer>().color = Color.white;
		SequenceTwoGameObject.GetComponent<SpriteRenderer>().color = Color.white;
		SequenceThreeGameObject.GetComponent<SpriteRenderer>().color = Color.white;
	}
}
