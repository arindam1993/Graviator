using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Audio; 

public class PlaySoundFX : MonoBehaviour {
	
	public AudioClip[] sfx; 
	public Button playFX; 

	// Use this for initialization
	void Start () {
		Button p_fx = playFX.GetComponent<Button> ();
		p_fx.onClick.AddListener (playSFX); 
	}

	void playSFX() {
		SoundFXManager.RandomizeSfx(gameObject.GetComponent<AudioSource> (),sfx); 
		// SoundFXManager.playSingleFX(gameObject.GetComponent<AudioSource> (),sfx[4]); 
	}



	// Update is called once per frame
	void Update () {
		
	}
}
