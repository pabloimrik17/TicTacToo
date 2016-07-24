using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

	public Slider volumenSlider;
	public Slider difficultySlider;
	public LevelManager levelManager;

	private MusicManager musicManager;

	// Use this for initialization
	void Start () {
		musicManager = GameObject.FindObjectOfType<MusicManager>();
		GetPlayerPrefs();
	}

	void Update() {
		musicManager.SetVolume(volumenSlider.value);
	}
	
	public void SaveAndExit() {
		PlayerPrefManager.SetMasterVolume(volumenSlider.value);	
		PlayerPrefManager.SetDifficulty(difficultySlider.value);
		levelManager.LoadLevel("Start");
	}

	public void GetPlayerPrefs() {
		volumenSlider.value = PlayerPrefManager.GetMasterVolume();
		difficultySlider.value = PlayerPrefManager.GetDifficulty();
	}

	public void RestorePlayerPrefs() {
		PlayerPrefManager.SetMasterVolume(0.8f);	
		PlayerPrefManager.SetDifficulty(2);
	}
}
