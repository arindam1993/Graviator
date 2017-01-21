using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;

	void Awake () {
		if (instance == null) {
			instance = this; 
		} else if (instance != this) {
			Destroy (gameObject); 
		}

		DontDestroyOnLoad (gameObject); 
	}
		
	/** To Use: 
	 * SoundManager.PlaySingle(tracks[0]); 
	 */
	public static void PlaySingle(AudioClip clip) {
		AudioSource efxSource = instance.gameObject.GetComponent<AudioSource> ();
		efxSource.clip = clip; 
		efxSource.Stop (); 
		efxSource.Play (); 
	}

	/** To Use: 
	 * SoundManager.playSingleFX(gameObject.GetComponent<AudioSource> (),track); 
	 */
	public static void playSingleFX(AudioSource src, AudioClip clip) {
		AudioSource efxSource = src; 
		efxSource.clip = clip;
		efxSource.Play(); 
	}
		
	/** To Use: 
	 * SoundManager.RandomizeSfx(gameObject.GetComponent<AudioSource> (),tracksArray); 
	 */
	public static void RandomizeSfx (AudioSource source, params AudioClip[] clips) {
		AudioSource efxSource = source; 
		int randomIndex = Random.Range (0, clips.Length); 
		efxSource.clip = clips [randomIndex]; 
		efxSource.Play (); 
	}
		
	/** To Use: 
	 * SoundManager.SwapSnapshotFX (isFiltered, normal, filtered);
	 * isFiltered = !isFiltered; 
	*/
	public static void SwapSnapshotFX(bool isFiltered, 
									AudioMixerSnapshot normal, AudioMixerSnapshot filtered) {
		if (isFiltered) {
			normal.TransitionTo (.2f); 
			isFiltered = false;
		} else {
			filtered.TransitionTo (.2f); 
			isFiltered = true; 
		}
	}

	/** To Use: 
	 * Add your master mixer so you can pass it to next new object
	 * public AudioMixerGroup mixer;
	 * SoundManager.CrossFade(tracks[currentTrack], output, fadeTime); 
	 */
	public static void CrossFade(AudioClip newTrack, AudioMixerGroup output, float fadeTime=1.0f) {
		instance.StopAllCoroutines (); 
		AudioSource newAudioSource = instance.gameObject.AddComponent<AudioSource> ();
		newAudioSource.volume = 0.0f; 
		newAudioSource.clip = newTrack;
		newAudioSource.outputAudioMixerGroup = output; 
		newAudioSource.Play (); 
		instance.StartCoroutine(instance.ActuallyCrossfade(newAudioSource,fadeTime)); 
	}

	IEnumerator ActuallyCrossfade(AudioSource newSource, float fadeTime) {
		float t = 0.0f;
		while (t < fadeTime) {
			newSource.volume = Mathf.Lerp (0.0f, 1.0f, t / fadeTime); 
			GetComponent<AudioSource>().volume =  1.0f - newSource.volume;
			t += Time.deltaTime;
			yield return null; 
		}
		newSource.volume = 1.0f; 
		Destroy (GetComponent<AudioSource>()); 
	}
		
}
