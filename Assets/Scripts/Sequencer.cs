using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sequencer : MonoBehaviour {
	public float BeatsPerMinute = 80;
	public int MeasureLength = 3;
	public int CurrentBeat = 0;
	public int CurrentMeasure = 0;
	public bool IsRunning = false;
	public float TimeRunning = 0.0f;

	private List<AudioClip> audioSequences = new List<AudioClip>();
	public int previousBeat = -1;
	public int previousMeasure = -1;
	public float beatPercentage = 0.0f;
	public float measurePercentage = 0.0f;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 3; i++) {
			AudioClip clip = Resources.Load ("Music/Sequence" + i) as AudioClip;
			audioSequences.Add (clip);
		}

		StartSequencer ();
	}

	public void StartSequencer()
	{
		if (!IsRunning) {
			CurrentBeat = 0;
			CurrentMeasure = 0;
			previousBeat = -1;
			previousMeasure = -1;
			TimeRunning = 0.0f;
		}
		IsRunning = true;
	}

	public void StopSequencer()
	{
		IsRunning = false;
	}

	public bool IsBeatChangeFrame(){
		return previousBeat != CurrentBeat;
	}

	public bool IsMeasureChangeFrame(){
		return previousMeasure != CurrentMeasure;
	}

	public float GetBeatPercentage(){
		return beatPercentage;
	}

	public float GetMeasurePercentage(){
		return measurePercentage;
	}

	// Update is called once per frame
	void Update () {
		UpdateTiming ();
		UpdateSounds ();
	}

	private void UpdateTiming() {
		previousBeat = CurrentBeat;
		previousMeasure = CurrentMeasure;

		float beatDuration = 60.0f / BeatsPerMinute;

		TimeRunning += Time.deltaTime;
		CurrentBeat = ((int)Mathf.Floor (TimeRunning / beatDuration)) % MeasureLength;
		CurrentMeasure = ((int)Mathf.Floor (TimeRunning / beatDuration)) / 3;
		beatPercentage = Mathf.Repeat (TimeRunning, beatDuration);
		measurePercentage = Mathf.Repeat (TimeRunning, beatDuration * MeasureLength);
	}

	void UpdateSounds() {
		if (IsMeasureChangeFrame ()) {
			AudioSource audio = GetComponent<AudioSource> ();

			audio.Stop ();
			audio.clip = audioSequences [Random.Range (0, 3)];
			audio.Play ();
		}
	}
}
