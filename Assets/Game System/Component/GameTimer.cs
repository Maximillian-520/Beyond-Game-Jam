using System;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public event EventHandler OnGameTimerFinished;
    public event EventHandler OnChoresTimerFired;

    [Header("Timer Settings")]
    [SerializeField] private float gameTime = 180.0f;
    [SerializeField] private float minChoresTime = 50.0f;
    [SerializeField] private float maxChoresTime = 80.0f;

    private bool isGameTimerActive = false;
    private bool isChoresTimerActive = false;
    private float gameTimer = 0.0f;
    private float choresTimer = 0.0f;
    
    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Update()
    {
        // Update game timer
        if (isGameTimerActive)
        {
            gameTimer -= Time.deltaTime;
            if (gameTimer <= 0)
            {
                OnGameTimerFinished.Invoke(this, EventArgs.Empty);
                EndGameTimer();
            }
        }
        // Update chores timer
        if (isChoresTimerActive)
        {
            choresTimer -= Time.deltaTime;
            if (choresTimer <= 0)
            {
                OnChoresTimerFired.Invoke(this, EventArgs.Empty);
                ResetChoresTimer();
            }
        }
    }
    #endregion

    // ====================================================================================================
    //                     Timer Functions
    // ====================================================================================================
    #region Timer
    public void StartGameTimer()
    {
        gameTimer = gameTime;
        ResetChoresTimer();
        ResumeGameTimer();
    }

    public void EndGameTimer() {PauseGameTimer();}

    public void ResumeGameTimer()
    {
        isGameTimerActive = true;
        isChoresTimerActive = true;
    }

    public void PauseGameTimer()
    {
        isGameTimerActive = false;
        isChoresTimerActive = false;
    }

    private void ResetChoresTimer()
    {
        choresTimer = UnityEngine.Random.Range(minChoresTime, maxChoresTime);
    }
    #endregion
}
