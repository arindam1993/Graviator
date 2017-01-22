using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Audio; 

public class PlayMusic: MonoBehaviour {

	public AudioClip[] tracks; 
	public AudioMixerGroup mixer;
	public AudioMixerSnapshot normal; 
	public AudioMixerSnapshot filtered;

	public Button playClips; 
	public Button snapShotFilterButton; 
	public float fadeTime = 1.0f; 
	private int currentTrack = 0; 
	private bool isFiltered = true; 

	// Use this for initialization
	void Start () {
		Button p_clips = playClips.GetComponent<Button> (); 
		p_clips.onClick.AddListener (playClippage); 

		Button snapShot_Button = snapShotFilterButton.GetComponent<Button> (); 
		snapShot_Button.onClick.AddListener (filterStuff); 
	}

	void playClippage(){
		currentTrack++; 
		if (currentTrack >= tracks.Length) {
			currentTrack = 0; 
		}
		MusicManager.CrossFade(tracks[currentTrack], mixer, fadeTime); 
		// MusicManager.PlaySingle(tracks[1]); 
	}


	void filterStuff() {
		MusicManager.SwapSnapshotFX (isFiltered, normal, filtered);
		isFiltered = !isFiltered; 
	}


	// Update is called once per frame
	void Update () {
	}
}
