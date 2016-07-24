using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;
	private AudioSource audioSource;

	void Awake() {
		DontDestroyOnLoad (gameObject);
	}
	// Use this for initialization
	void Start() {
		audioSource = gameObject.GetComponent<AudioSource> ();
		playLevelAudio (0);
	}

	void OnLevelWasLoaded(int nivel) {
		playLevelAudio (nivel);
	}

	private void playLevelAudio(int audioIndex) {   
		if (levelMusicChangeArray [audioIndex]) {
			AudioClip thisLevelAudio = levelMusicChangeArray [audioIndex];
			audioSource.clip = thisLevelAudio;
			if (audioIndex > 0) {
				audioSource.loop = true;
			}
			audioSource.Play ();
		}
	}


	public void SetVolume(float volume) {
		audioSource.volume = volume;
	}
}
