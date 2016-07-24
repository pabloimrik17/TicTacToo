using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPrefManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";

	public static float GetMasterVolume () {
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}

	public static void SetMasterVolume (float volume) {
		if (volume > 0f && volume <= 1f) {
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError("Master Volume Out of Range");
		}
	}

	public static float GetDifficulty () {
		return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
	}

	public static void SetDifficulty(float difficulty) {
		if (difficulty >= 1f && difficulty <= 3f) {
			PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
		} else {
			Debug.Log("Trying to set wrong difficulty");
		}
	}

	public static bool IsLevelUnlocked(int level) {
		if (level <= GetRealLevelNumber () || level > GetRealLevelNumber ()) {

			int unlocked = PlayerPrefs.GetInt (LEVEL_KEY);
			bool isUnlocked = (unlocked == 1);

			return isUnlocked;

		} else {
			Debug.Log("Trying to get wrong level");
			return false;
		}
	}

	public static void UnlockLevel (int level) {
		if (level <= GetRealLevelNumber() || level < GetRealLevelNumber() ) {
			PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1); // 1 unlocked
		} else {
			Debug.LogError("Trying to unlock wrong level");
		}
	}

	public static int GetRealLevelNumber() {
		return SceneManager.sceneCountInBuildSettings - 1;
	}

}
