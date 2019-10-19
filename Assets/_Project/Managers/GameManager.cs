using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class GameManager : MonoBehaviour
{
    #region Properties
    public bool GameIsPaused { get; private set;}
    public bool Recording { get; private set; }
    #endregion

    #region Fields
    float storedFixedDeltaTime;
    #endregion

    // Update is called once per frame
    void Update () 
	{
		if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            Recording = false;
        }
        else
        {
            Recording = true;
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            TogglePauseGame();
        }
	}

    private void TogglePauseGame ()
    {
        if (!GameIsPaused)
        {
            // Store current fixedDeltaTime.
            storedFixedDeltaTime = Time.fixedDeltaTime;

            // Pause game.
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
            print("Game paused.");
        }
        else
        {
            // Resume game.
            Time.timeScale = 1;
            Time.fixedDeltaTime = storedFixedDeltaTime;
            print("Game resumed.");
        }
        GameIsPaused = !GameIsPaused;
    }

    /// <summary>
    /// React to the application itself pausing.
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause (bool pause)
    {
        if (!GameIsPaused && pause)
        {
            TogglePauseGame();
        }
    }
}
