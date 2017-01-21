using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour {

	public static SoundFXManager instance = null;

	void Awake () {
		if (instance == null) {
			instance = this; 
		} else if (instance != this) {
			Destroy (gameObject); 
		}

		DontDestroyOnLoad (gameObject); 
	}

	/** To Use: 
	 * SoundFXManager.playSingleFX(gameObject.GetComponent<AudioSource> (),track); 
	 */
	public static void playSingleFX(AudioSource src, AudioClip clip) {
		AudioSource efxSource = src; 
		efxSource.clip = clip;
		efxSource.Play(); 
	}

	/** To Use: 
	 * SoundFXManager.RandomizeSfx(gameObject.GetComponent<AudioSource> (),tracksArray); 
	 */
	public static void RandomizeSfx (AudioSource source, params AudioClip[] clips) {
		AudioSource efxSource = source; 
		int randomIndex = Random.Range (0, clips.Length); 
		efxSource.clip = clips [randomIndex]; 
		efxSource.Play (); 
	}

}
