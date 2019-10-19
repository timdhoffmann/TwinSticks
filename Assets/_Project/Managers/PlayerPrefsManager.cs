using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";

    //
    // Using static methods to be able to access them from anywhere (using "PlayerPrefsManger.Method").
    //

    private void Start ()
    {
        // Deletes all permanently stored playerprefes. Caution!
        // PlayerPrefs.DeleteAll();
    }

    #region Volume
    /// <summary>
    /// Set the master volume.
    /// </summary>
    /// <param name="volume"></param>
    public static void SetMasterVolume (float volume) {
		if (volume >= 0f && volume <= 1f) {
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError("Master volume out of range (between 0 and 1).");
		}
	}

    /// <summary>
    /// Get the master volume.
    /// </summary>
    /// <returns></returns>
	public static float GetMasterVolume () {
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}
    #endregion

    #region Level unlocking
    /// <summary>
    /// Unlocks a level.
    /// </summary>
    /// <param name="level"></param>
    public static void UnlockLevel (int level) {
		if (level <= SceneManager.sceneCountInBuildSettings - 1) {
			PlayerPrefs.SetInt (LEVEL_KEY + level.ToString (), 1); // User 1 for true.
		} else {
			Debug.LogError ("Trying to unlock level not included in build.");
		}
	}

    /// <summary>
    /// Check if a level is unlocked.
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
	public static bool IsLevelUnlocked (int level) {
		int levelValue = PlayerPrefs.GetInt (LEVEL_KEY + level.ToString ());
		bool isLevelUnlocked = (levelValue == 1); // Return true if levelValue == 1.

		if (level <= SceneManager.sceneCountInBuildSettings -1) {
			return isLevelUnlocked;
		} else {
			Debug.LogError ("Trying to query level not included in build.");
			return false;
		}
	}
    #endregion

    #region Difficulty
    public static void SetDifficulty (float difficulty) {
		if (difficulty >= 1f && difficulty <= 3f) {
			PlayerPrefs.SetFloat (DIFFICULTY_KEY, difficulty);
		} else {
			Debug.LogError ("Trying to set difficulty out of range (between 1 and 3).");
		}
	}

	public static float GetDifficulty () {
		return PlayerPrefs.GetFloat (DIFFICULTY_KEY);
	}
    #endregion
}
